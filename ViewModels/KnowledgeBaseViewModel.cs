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
using Microsoft.Win32;

namespace TechSupport.ViewModels
{
    public class KnowledgeBaseViewModel : BaseViewModel
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;
    private string _statusMessage;

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
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
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

    public ICommand CreateArticleFromTicketCommand { get; }
    public ICommand ExportArticlesCommand { get; }
        

        public KnowledgeBaseViewModel(AppDbContext context, AuthService authService)
      {
            _context = context;
            _authService = authService;

            // Инициализация команд
            ChangeStatusCommand = new RelayCommandAsync(ChangeStatus);
            AssignUserCommand = new RelayCommandAsync(AssignUser);

            // Инициализация статуса
            StatusMessage = "Готово к работе";

            LoadInitialData();
        }

        private async Task CreateArticleFromTicket()
        {
            if (SelectedArticle == null) return;

            try
            {
                var newArticle = new KnowledgeArticle
                {
                    Title = SelectedArticle.Title,
                    Content = SelectedArticle.Description,
                    Status = "Черновик",
                    AuthorId = _authService.CurrentUser?.Id,
                    AssignedUserId = SelectedArticle.AssignedUserId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                };

                _context.KnowledgeArticles.Add(newArticle);
                await _context.SaveChangesAsync();

                // Помечаем заявку как преобразованную в статью
                SelectedArticle.Status = "Преобразована в статью";
                await _context.SaveChangesAsync();

                await LoadArticles();
                StatusMessage = "Статья успешно создана из заявки";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка создания статьи: {ex.Message}";
            }
        }

        private void ExportArticles()
        {
            var saveDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"KnowledgeBase_Export_{DateTime.Now:yyyyMMdd}.pdf"
            };

            if (saveDialog.ShowDialog() == true)
            {
                // Реализация экспорта в PDF
                ReportService.GenerateKnowledgeBaseReport(Articles.ToList(), saveDialog.FileName);
                StatusMessage = $"Экспорт завершен: {saveDialog.FileName}";
            }
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
            {
                StatusMessage = "Не выбрана статья или статус";
                return;
            }

            try
            {
                var oldStatus = SelectedArticle.Status;
                SelectedArticle.Status = SelectedStatus;
                SelectedArticle.LastUpdated = DateTime.UtcNow;

                await AddHistoryRecord(
                    SelectedArticle.Id,
                    "StatusChanged",
                    oldStatus,
                    SelectedStatus,
                    $"Статус изменён с {oldStatus} на {SelectedStatus}"
                );

                await _context.SaveChangesAsync();
                StatusMessage = $"Статус изменён на {SelectedStatus}";
                await LoadArticles();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка изменения статуса: {ex.Message}";
                Debug.WriteLine(ex);
            }
        }

        private async Task AssignUser()
        {
            if (SelectedArticle == null || SelectedUser == null)
            {
                StatusMessage = "Не выбрана статья или пользователь";
                return;
            }

            try
            {
                var oldUser = SelectedArticle.AssignedUser?.Name ?? "Не назначен";
                SelectedArticle.AssignedUserId = SelectedUser.Id;
                SelectedArticle.LastUpdated = DateTime.UtcNow;

                await AddHistoryRecord(
                    SelectedArticle.Id,
                    "Assigned",
                    oldUser,
                    SelectedUser.Name,
                    $"Ответственный изменён на {SelectedUser.Name}"
                );

                await _context.SaveChangesAsync();
                StatusMessage = $"Назначен ответственный: {SelectedUser.Name}";
                await LoadArticles();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка назначения: {ex.Message}";
                Debug.WriteLine(ex);
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
        internal Task LoadArticles()
        {
            // Загружаем только те поля, которые существуют в базе
            var articles = _context.KnowledgeArticles
                .Include(a => a.Author)
                .Select(a => new KnowledgeArticle
                {
                    Id = a.Id,
                    Title = a.Title,
                    Status = a.Status,
                    // Другие существующие поля...
                    Author = a.Author,
                    LastUpdated = a.LastUpdated
                    // Исключаем AssignedUserId и Description
                })
                .ToList();

            Articles.Clear();
            foreach (var article in articles)
            {
                Articles.Add(article);
            }

            return Task.CompletedTask;
        }

        // ... остальные методы ...
    }

}
