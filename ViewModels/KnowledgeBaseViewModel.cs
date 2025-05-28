using TechSupport.Context;
using TechSupport.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows;
using TechSupport.Views;
using System.Diagnostics;
using TechSupport.Services;
using System.Windows.Input;

namespace TechSupport.ViewModels
{
    public class KnowledgeBaseViewModel : BaseViewModel
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;
    
    // Существующие свойства
    public ObservableCollection<KnowledgeArticle> Articles { get; } = new();
    public ObservableCollection<string> Statuses { get; } = new() { "Новая", "В работе", "Решена", "Закрыта" };
    public ObservableCollection<User> TechSupportUsers { get; } = new();
    
    // Новые свойства для выбора
    private KnowledgeArticle _selectedArticle;
    public KnowledgeArticle SelectedArticle
    {
        get => _selectedArticle;
        set => SetProperty(ref _selectedArticle, value);
    }
    
    private string _selectedStatus;
    public string SelectedStatus
    {
        get => _selectedStatus;
        set => SetProperty(ref _selectedStatus, value);
    }
    
    private User _selectedUser;
    public User SelectedUser
    {
        get => _selectedUser;
        set => SetProperty(ref _selectedUser, value);
    }
    
    // Команды
    public ICommand ChangeStatusCommand { get; }
    public ICommand AssignUserCommand { get; }
    
    public KnowledgeBaseViewModel(AppDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
        
        // Инициализация команд
        ChangeStatusCommand = new RelayCommandAsync(ChangeStatus);
        AssignUserCommand = new RelayCommandAsync(AssignUser);
        
        LoadInitialData();
    }
    
    private async void LoadInitialData()
    {
        await LoadArticles();
        await LoadTechSupportUsers();
    }
    
    private async Task LoadTechSupportUsers()
    {
        var users = await _context.Users
            .Where(u => u.Role == "TechSupport" && u.IsActive)
            .ToListAsync();
        
        TechSupportUsers.Clear();
        foreach (var user in users)
        {
            TechSupportUsers.Add(user);
        }
    }
    
    private async Task ChangeStatus()
    {
        if (SelectedArticle == null || string.IsNullOrEmpty(SelectedStatus))
            return;
        
        try
        {
            var oldStatus = SelectedArticle.Status;
            SelectedArticle.Status = SelectedStatus;
            SelectedArticle.LastUpdated = DateTime.UtcNow;
            
            // Запись в историю
            await AddHistoryRecord(
                SelectedArticle.Id,
                "StatusChanged",
                oldStatus,
                SelectedStatus,
                $"Статус изменён с {oldStatus} на {SelectedStatus}"
            );
            
            await _context.SaveChangesAsync();
            await LoadArticles(); // Обновляем список
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка изменения статуса: {ex.Message}");
        }
    }
    
    private async Task AssignUser()
    {
        if (SelectedArticle == null || SelectedUser == null)
            return;
        
        try
        {
            var oldUserId = SelectedArticle.AuthorId;
            SelectedArticle.AuthorId = SelectedUser.Id;
            SelectedArticle.LastUpdated = DateTime.UtcNow;
            
            // Запись в историю
            await AddHistoryRecord(
                SelectedArticle.Id,
                "Assigned",
                oldUserId?.ToString(),
                SelectedUser.Id.ToString(),
                $"Ответственный изменён на {SelectedUser.Name}"
            );
            
                await _context.SaveChangesAsync();
                await LoadArticles(); // Обновляем список
            }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка назначения ответственного: {ex.Message}");
        }
    }
    
    private async Task AddHistoryRecord(int ticketId, string action, string oldValue, string newValue, string description)
    {
        _context.TicketHistories.Add(new TicketHistory
        {
            TicketId = ticketId,
            Action = action,
            OldValue = oldValue,
            NewValue = newValue,
            ChangedBy = _authService.CurrentUser?.Username ?? "System",
            ChangedByUserId = _authService.CurrentUser?.Id,
            ChangedAt = DateTime.UtcNow
        });
    }
        internal async Task LoadArticles()
        {
            // Загружаем статьи из базы данных
            var articles = _context.KnowledgeArticles
                .Include(a => a.Author) // Включаем автора статьи
                .ToList(); // Выполняем запрос и получаем список статей

            Articles.Clear(); // Очищаем текущую коллекцию статей
            foreach (var article in articles)
            {
                Articles.Add(article); // Добавляем загруженные статьи в коллекцию
            }
        }

        // ... остальные методы ...
    }

}
