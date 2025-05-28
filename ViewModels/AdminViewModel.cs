using TechSupport.Context;
using TechSupport.Models;
using TechSupport.Services;
using TechSupport.Views;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace TechSupport.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private string _statusMessage;

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<string> Roles { get; }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand AddUserCommand { get; }
        public ICommand SaveUsersCommand { get; }
        public ICommand ExportUsersCommand { get; }
        public ICommand RefreshStatsCommand { get; }

        public AdminViewModel()
        {
            _context = new AppDbContext();

            Roles = new ObservableCollection<string> { "Admin", "TechSupport", "User" };
            Users = new ObservableCollection<User>();

            AddUserCommand = new RelayCommand(AddUser);
            SaveUsersCommand = new RelayCommand(SaveUsers);
            ExportUsersCommand = new RelayCommand(ExportUsers);
            RefreshStatsCommand = new RelayCommand(LoadStatistics);

            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            foreach (var user in _context.Users.ToList())
            {
                Users.Add(user);
            }
            StatusMessage = $"Загружено пользователей: {Users.Count}";
        }

        private void AddUser()
        {
            var newUser = new User
            {
                Username = "Новый пользователь",
                Name = "Новый пользователь",
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            Users.Add(newUser);
            StatusMessage = "Добавлен новый пользователь";
        }

        private void SaveUsers()
        {
            try
            {
                _context.SaveChanges();
                StatusMessage = "Изменения сохранены успешно";
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"Ошибка сохранения: {ex.Message}";
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportUsers()
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

        private void LoadStatistics()
        {
            // Здесь будет загрузка статистики
            StatusMessage = "Статистика обновлена";
        }
    }
}
