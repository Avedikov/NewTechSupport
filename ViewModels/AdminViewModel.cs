using TechSupport.Context;
using TechSupport.Models;
using TechSupport.Services;
using TechSupport.Views;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Text;

namespace TechSupport.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private string _statusMessage;
        private User _selectedUser;
        private bool _isCheckingAdmin = false;

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<string> Roles { get; } = new ObservableCollection<string> { "Admin", "TechSupport", "User" };
        public DateTime? StatsDateFrom { get; set; }
        public DateTime? StatsDateTo { get; set; }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public ICommand AddUserCommand { get; }
        public ICommand SaveUsersCommand { get; }
        public ICommand ExportUsersCommand { get; }
        public ICommand RefreshStatsCommand { get; }
        public ICommand MakeAdminCommand { get; }
        

        public AdminViewModel(AppDbContext context)
        {
            _context = context;

            Roles = new ObservableCollection<string> { "Admin", "TechSupport", "User" };
            Users = new ObservableCollection<User>();

            AddUserCommand = new RelayCommand(ExecuteAddUser);
            SaveUsersCommand = new RelayCommand(ExecuteSaveUsers);
            ExportUsersCommand = new RelayCommand(ExecuteExportUsers);
            RefreshStatsCommand = new RelayCommand(ExecuteRefreshStats);
            MakeAdminCommand = new RelayCommand(ExecuteMakeAdmin, CanExecuteMakeAdmin);

            // Асинхронная инициализация данных
            Task.Run(async () =>
            {
                await EnsureAdminExists();
                await LoadUsers();
            }).ConfigureAwait(false);
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _context.Users
                    .AsNoTracking()
                    .ToListAsync();

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Users.Clear();
                    foreach (var user in users)
                    {
                        Users.Add(user);
                    }
                    StatusMessage = $"Загружено пользователей: {Users.Count}";
                });
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка загрузки: {ex.Message}";
            }
        }

        private async Task EnsureAdminExists()
        {
            if (_isCheckingAdmin) return;
            _isCheckingAdmin = true;

            try
            {
                using (var separateContext = new AppDbContext())
                {
                    bool adminExists = await separateContext.Users
                        .AsNoTracking()
                        .AnyAsync(u => u.Role == "Admin");

                    if (!adminExists)
                    {
                        var admin = new User
                        {
                            Username = "admin",
                            Name = "Администратор",
                            Email = "admin@example.com",
                            Role = "Admin",
                            IsActive = true,
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                            CreatedAt = DateTime.UtcNow
                        };

                        separateContext.Users.Add(admin);
                        await separateContext.SaveChangesAsync();

                        // Обновляем основной контекст
                        var newAdmin = await separateContext.Users
                            .AsNoTracking()
                            .FirstAsync(u => u.Username == "admin");

                        _context.Users.Add(newAdmin);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            finally
            {
                _isCheckingAdmin = false;
            }
        }

        private void ExecuteAddUser()
        {
            var newUser = new User
            {
                Username = "newuser_" + DateTime.Now.ToString("HHmmss"),
                Name = "Новый пользователь",
                Email = "temp@example.com", // Добавлено обязательное поле
                Department = "Не указан",
                Position = "Не указана",
                Role = "User",
                IsActive = true,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("temp123"),
                CreatedAt = DateTime.UtcNow,
                
            };

            _context.Users.Add(newUser);
            Users.Add(newUser);
            StatusMessage = "Добавлен новый пользователь (пароль: temp123)";
        }

        private bool CanExecuteMakeAdmin()
        {
            return SelectedUser != null && SelectedUser.Role != "Admin";
        }

        private void ExecuteMakeAdmin()
        {
            if (SelectedUser != null)
            {
                SelectedUser.Role = "Admin";
                StatusMessage = $"{SelectedUser.Name} теперь администратор";
                CommandManager.InvalidateRequerySuggested();
                OnPropertyChanged(nameof(Users));
            }
        }

        private async void ExecuteSaveUsers()
        {
            try
            {
                // Валидация перед сохранением
                foreach (var user in Users.Where(u => _context.Entry(u).State == EntityState.Added))
                {
                    if (string.IsNullOrWhiteSpace(user.Username))
                    {
                        StatusMessage = "Ошибка: Логин не может быть пустым";
                        return;
                    }

                    if (_context.Users.Any(u => u.Id != user.Id && u.Username == user.Username))
                    {
                        StatusMessage = $"Ошибка: Логин {user.Username} уже существует";
                        return;
                    }
                }

                // Сохранение с обработкой конфликтов
                int changes = await _context.SaveChangesAsync();
                StatusMessage = $"Успешно сохранено. Изменений: {changes}";
            }
            catch (DbUpdateException ex)
            {
                string errorMessage = GetDetailedErrorMessage(ex);
                StatusMessage = $"Ошибка сохранения: {errorMessage}";
                MessageBox.Show($"Ошибка сохранения: {errorMessage}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка: {ex.Message}";
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetDetailedErrorMessage(DbUpdateException ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine(ex.Message);

            if (ex.InnerException != null)
            {
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(ex.InnerException.Message);

                // Для SQL Server
                if (ex.InnerException is SqlException sqlEx)
                {
                    sb.AppendLine("SQL Error:");
                    foreach (SqlError error in sqlEx.Errors)
                    {
                        sb.AppendLine($"- {error.Message}");
                    }
                }
            }

            return sb.ToString();
        }

        private void ExecuteExportUsers()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Users_Report_{DateTime.Now:yyyyMMdd}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                ReportService.GenerateUserReport(Users.ToList(), saveFileDialog.FileName);
                StatusMessage = $"Отчет сохранен: {saveFileDialog.FileName}";
            }
        }

        private void ExecuteRefreshStats()
        {
            StatusMessage = "Статистика обновлена";
            // Здесь будет загрузка статистики
        }
    }
}
