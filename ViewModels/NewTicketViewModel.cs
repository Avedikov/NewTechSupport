using TechSupport.Context;
using TechSupport.Models;
using TechSupport.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace TechSupport.ViewModels
{
    public class NewTicketViewModel : BaseViewModel
    {
        private readonly TicketService _ticketService;
        private readonly AuthService _authService;
        private readonly AppDbContext _context;
        private string _filePath;
        private int? _assignedUserId;

        public string Subject { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public int? AssignedUserId
        {
            get => _assignedUserId;
            set => SetProperty(ref _assignedUserId, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public ObservableCollection<User> TechSupportUsers { get; } = new();

        public ICommand AttachFileCommand { get; }
        public ICommand CreateTicketCommand { get; }
        public ICommand LoadTechSupportUsersCommand { get; }

        public NewTicketViewModel(TicketService ticketService, AuthService authService, AppDbContext context)
        {
            _ticketService = ticketService;
            _authService = authService;
            _context = context;

            AttachFileCommand = new RelayCommand(AttachFile);
            CreateTicketCommand = new RelayCommand(CreateTicket);
            LoadTechSupportUsersCommand = new RelayCommand(LoadTechSupportUsers);

            LoadTechSupportUsers();
        }

        private void LoadTechSupportUsers()
        {
            try
            {
                var users = _context.Users
                    .Where(u => u.Role == "TechSupport" || u.Role == "Administrator")
                    .OrderBy(u => u.Name)
                    .ToList();

                TechSupportUsers.Clear();
                foreach (var user in users)
                {
                    TechSupportUsers.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка ответственных: {ex.Message}");
            }
        }

        private void AttachFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string destPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Attachments",
                        Path.GetFileName(openFileDialog.FileName));

                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                    File.Copy(openFileDialog.FileName, destPath, true);
                    FilePath = destPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка прикрепления файла: {ex.Message}");
                }
            }
        }

        private bool CanCreateTicket()
        {
            return !string.IsNullOrWhiteSpace(Subject) &&
                   !string.IsNullOrWhiteSpace(Description) &&
                   !string.IsNullOrWhiteSpace(ClientName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(Email);
        }

        private void CreateTicket()
        {
            if (!CanCreateTicket())
            {
                MessageBox.Show("Заполните все обязательные поля корректно!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newTicket = new Ticket
                {
                    Subject = Subject,
                    Description = Description,
                    ClientName = ClientName,
                    Email = Email,
                    Status = "Новая",
                    CreateDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    AssignedUserId = AssignedUserId,
                    AuthorId = _authService.CurrentUser?.Id,  // Устанавливаем текущего пользователя как автора
                    AttachedFilePath = FilePath
                };

                _context.Tickets.Add(newTicket);

                // Добавляем запись в историю
                //_context.TicketHistories.Add(new TicketHistory
                //{
                //    Ticket = newTicket,
                //    Action = "Created",
                //    Description = "Заявка создана",
                //    ChangedAt = DateTime.UtcNow,
                //    ChangedByUserId = _authService.CurrentUser?.Id
                //});

                _context.SaveChanges();

                MessageBox.Show("Заявка успешно создана!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания заявки: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            Subject = string.Empty;
            Description = string.Empty;
            ClientName = string.Empty;
            Email = string.Empty;
            FilePath = string.Empty;
            AssignedUserId = null;

            OnPropertyChanged(nameof(Subject));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(ClientName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(FilePath));
        }
    }
}
