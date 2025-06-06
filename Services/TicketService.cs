using TechSupport.Context;
using TechSupport.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static TechSupport.Models.TicketFilter;

namespace TechSupport.Services
{
    public class TicketService
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;
        private AppDbContext context;



        public TicketService(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }



        // Получение отфильтрованного списка заявок
        public async Task<List<Ticket>> GetFilteredTicketsAsync(TicketFilter filter)
        {
            var query = _context.Tickets
                .Include(t => t.AssignedUser)
                .Include(t => t.History)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(t => t.Status == filter.Status);

            if (filter.AssignedUserId.HasValue)
                query = query.Where(t => t.AssignedUserId == filter.AssignedUserId);

            if (filter.DateFrom.HasValue)
                query = query.Where(t => t.CreateDate >= filter.DateFrom.Value.Date);

            if (filter.DateTo.HasValue)
                query = query.Where(t => t.CreateDate <= filter.DateTo.Value.Date.AddDays(1));

            return await query.OrderByDescending(t => t.LastUpdated).ToListAsync();
        }
        public async Task<List<Ticket>> GetFilteredTicketsWithUsersAsync(TicketFilter filter)
        {
            return await _context.Tickets
                .Include(t => t.AssignedUser) // Важно: загружаем связанного пользователя
                .Where(t => (filter.Status == null || t.Status == filter.Status) &&
                           (filter.AssignedUserId == null || t.AssignedUserId == filter.AssignedUserId))
                .ToListAsync();
        }

        // Создание новой заявки
        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            try
            {
                ticket.CreateDate = DateTime.UtcNow;
                ticket.LastUpdated = DateTime.UtcNow;

                _context.Tickets.Add(ticket);

                _context.TicketHistories.Add(new TicketHistory
                {
                    Ticket = ticket,
                    Action = "Created",
                    NewValue = "Новая заявка",
                    ChangedBy = _authService.CurrentUser?.Username ?? "System",
                    ChangedByUserId = _authService.CurrentUser?.Id // Теперь Id доступен
                });

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .AsNoTracking() // Оптимизация для чтения
                .ToListAsync(); // Асинхронный метод!
        }

        // Обновление заявки
        public async Task<bool> UpdateTicketAsync(Ticket ticket, User _currentUser)
        {
            try
            {
                var existingTicket = await _context.Tickets.FindAsync(ticket.Id);
                if (existingTicket == null)
                    return false;

                // Запоминаем старые значения для истории
                var oldStatus = existingTicket.Status;
                var oldAssignedUserId = existingTicket.AssignedUserId;

                // Обновляем поля
                _context.Entry(existingTicket).CurrentValues.SetValues(ticket);
                existingTicket.LastUpdated = DateTime.UtcNow;

                // Логируем изменения
                if (oldStatus != ticket.Status)
                {
                    await AddHistoryRecordAsync(existingTicket, "Status Changed",
                        $"Статус изменён с {oldStatus} на {ticket.Status}");
                }

                if (oldAssignedUserId != ticket.AssignedUserId)
                {
                    var newAssignee = await _context.Users.FindAsync(ticket.AssignedUserId);
                    await AddHistoryRecordAsync(existingTicket, "Assignee Changed",
                        $"Исполнитель изменён на {newAssignee?.Username ?? "Не назначен"}");
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка обновления заявки: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ChangeStatusAsync(int ticketId, string newStatus, User currentUser)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null || !ticket.CanUserEdit(currentUser))
                return false;

            var oldStatus = ticket.Status;
            ticket.Status = newStatus;
            ticket.LastUpdated = DateTime.UtcNow;

            await AddHistoryRecordAsync(ticket, "StatusChanged",
                $"Статус изменен с {oldStatus} на {newStatus}", currentUser);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Ticket>> SearchTicketsAsync(TicketFilter filter)
        {
            var query = _context.Tickets
                .Include(t => t.AssignedUser)
                .AsQueryable();

            // Базовые фильтры
            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(t => t.Status == filter.Status);

            if (filter.AssignedUserId.HasValue)
                query = query.Where(t => t.AssignedUserId == filter.AssignedUserId);

            // Комплексный поиск
            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                query = query.Where(t =>
                    t.Id.ToString().Contains(filter.SearchText) || // ID заявки
                    (t.AssignedUser != null && t.AssignedUser.Name.Contains(filter.SearchText)) || // ФИО ответственного
                    t.AssignedUserId.ToString().Contains(filter.SearchText) || // ID ответственного
                    t.ClientName.Contains(filter.SearchText) || // Фамилия клиента
                    t.Subject.Contains(filter.SearchText)); // Тема заявки
            }
            if (filter.DateFrom.HasValue)
            {
                query = filter.DateFilterType == DateFilterType.CreateDate
                    ? query.Where(t => t.CreateDate >= filter.DateFrom.Value.Date)
                    : query.Where(t => t.LastUpdated >= filter.DateFrom.Value.Date);
            }

            if (filter.DateTo.HasValue)
            {
                var dateTo = filter.DateTo.Value.Date.AddDays(1);
                query = filter.DateFilterType == DateFilterType.CreateDate
                    ? query.Where(t => t.CreateDate < dateTo)
                    : query.Where(t => t.LastUpdated < dateTo);
            }

            // Дополнительные фильтры
            if (!string.IsNullOrEmpty(filter.ClientNameSearch))
                query = query.Where(t => t.ClientName.Contains(filter.ClientNameSearch));

            if (!string.IsNullOrEmpty(filter.SubjectSearch))
                query = query.Where(t => t.Subject.Contains(filter.SubjectSearch));

            return await query
                .OrderByDescending(t => t.CreateDate)
                .ToListAsync();
        }

        // Удаление заявки
        public async Task<bool> DeleteTicketAsync(int id)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    return false;

                _context.Tickets.Remove(ticket);
                await AddHistoryRecordAsync(ticket, "Deleted", "Заявка удалена");

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка удаления заявки: {ex.Message}");
                return false;
            }
        }

        // Получение заявки по ID
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.AssignedUser)
                .Include(t => t.History)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // Добавление записи в историю
        private async Task AddHistoryRecordAsync(Ticket ticket, string action, string newValue)
        {
            _context.TicketHistories.Add(new TicketHistory
            {
                TicketId = ticket.Id,
                Action = action,
                NewValue = newValue,
                ChangedBy = _authService.CurrentUser?.Username ?? "System",
                ChangedByUserId = _authService.CurrentUser?.Id,
                ChangedAt = DateTime.UtcNow
            });
        }

        public bool CreateTicket(Ticket ticket)
        {
            try
            {
                if (ticket == null)
                    throw new ArgumentNullException(nameof(ticket));

                _context.Tickets.Add(ticket);
                int result = _context.SaveChanges();

                // Проверяем, что хотя бы одна запись была сохранена
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                // Логируем внутреннее исключение (часто содержит детали ошибки БД)
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"Ошибка базы данных: {errorMessage}");
                MessageBox.Show($"Ошибка БД: {errorMessage}"); // Показываем пользователю
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
                return false;
            }
        }
        private async Task AddHistoryRecordAsync(Ticket ticket, string action, string newValue, User currentUser)
        {
            _context.TicketHistories.Add(new TicketHistory
            {
                TicketId = ticket.Id,
                Action = action,
                NewValue = newValue,
                ChangedBy = currentUser?.Username ?? "System",
                ChangedByUserId = currentUser?.Id,
                ChangedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }





        // Дополнительные методы по необходимости...
    }
}
