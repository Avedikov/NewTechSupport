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
        private string _filePath;

        public string Subject { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public ICommand AttachFileCommand { get; }
        public ICommand CreateTicketCommand { get; }

        public NewTicketViewModel(TicketService ticketService)
        {
            _ticketService = ticketService;
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
                   !string.IsNullOrWhiteSpace(ClientName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(Email);
        }

        private void CreateTicket()
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(Subject))
            {
                MessageBox.Show("Заполните тему заявки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Заполните описание заявки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var newTicket = new Ticket
                {
                    ClientName = ClientName,
                    Email = Email,
                    Subject = Subject,
                    Description = Description,
                    Status = "Новая",
                    CreateDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                };

                bool isSuccess = _ticketService.CreateTicket(newTicket);

                if (isSuccess)
                {
                    MessageBox.Show("Заявка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Subject = "";
                    Description = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            Subject = string.Empty;
            Description = string.Empty;
            ClientName = string.Empty;
            Email = string.Empty;
            FilePath = string.Empty;
            OnPropertyChanged(nameof(Subject));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(ClientName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(FilePath));
        }
    }
}
