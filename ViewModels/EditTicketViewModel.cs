using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TechSupport.Models;
using TechSupport.Services;
using TechSupport.Views;

namespace TechSupport.ViewModels
{
    public class EditTicketViewModel : BaseViewModel
    {
        private readonly TicketService _ticketService;
        private readonly User _currentUser;
        public Action<Ticket> OnSaveCallback { get; set; }

        public Ticket Ticket { get; }
        public ObservableCollection<string> Statuses { get; } = new ObservableCollection<string>
    {
        "Новая", "В работе", "Решена", "Закрыта"
    };

        public ICommand SaveCommand { get; }

        public EditTicketViewModel(TicketService ticketService, Ticket ticket, User currentUser)
        {
            _ticketService = ticketService;
            _currentUser = currentUser;
            Ticket = ticket;

            SaveCommand = new RelayCommand(async () => await SaveChanges());
        }

        private async Task SaveChanges()
        {
            if (!Ticket.CanUserEdit(_currentUser))
            {
                NotificationService.ShowError("Нет прав на редактирование");
                return;
            }

            var success = await _ticketService.UpdateTicketAsync(Ticket, _currentUser);
            if (success)
            {
                OnSaveCallback?.Invoke(Ticket);
                CloseWindow();
            }
        }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<EditTicketWindow>()
                .FirstOrDefault(w => w.DataContext == this)?.Close();
        }
    }
}
