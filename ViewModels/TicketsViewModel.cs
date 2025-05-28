using TechSupport.Models;
using TechSupport.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace TechSupport.ViewModels
{
    public class TicketsViewModel : BaseViewModel
    {
        private readonly TicketService _ticketService;
        private readonly AuthService _authService;

        private Ticket _selectedTicket;
        public Ticket SelectedTicket
        {
            get => _selectedTicket;
            set => SetProperty(ref _selectedTicket, value);
        }

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        public ObservableCollection<string> Statuses { get; }
        public ObservableCollection<Ticket> Tickets { get; }
        public ICommand LoadTicketsCommand { get; }

        public ICommand FilterCommand { get; }


        public bool IsLoading { get; private set; }


        public TicketsViewModel(TicketService ticketService, AuthService authService)
        {
            _ticketService = ticketService;
            _authService = authService;

           

            Statuses = new ObservableCollection<string> { "Все", "Новая", "В работе", "Решена", "Закрыта" };
            Tickets = new ObservableCollection<Ticket>();

            FilterCommand = new RelayCommandAsync(LoadTicketsAsync); // Используем асинхронную команду
            LoadTicketsAsync();
        }

        private void LoadTickets()
        {
            var filter = new TicketFilter
            {
                Status = SelectedStatus == "Все" ? null : SelectedStatus,
                // Используем экземпляр authService вместо статического доступа
                AssignedUserId = _authService.IsTechSupport ? _authService.CurrentUser?.Id : null
            };

            var tickets = _ticketService.GetFilteredTicketsAsync(filter).Result;

            Tickets.Clear();
            foreach (var ticket in tickets)
            {
                Tickets.Add(ticket);
            }
        }

        private async Task LoadTicketsAsync()
        {
            try
            {
                var filter = new TicketFilter
                {
                    Status = SelectedStatus == "Все" ? null : SelectedStatus,
                    AssignedUserId = _authService.IsTechSupport ? _authService.CurrentUser?.Id : null
                };

                var tickets = await _ticketService.GetFilteredTicketsAsync(filter);

                Tickets.Clear();
                foreach (var ticket in tickets)
                {
                    Tickets.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

    }
}
