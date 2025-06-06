using TechSupport.Models;
using TechSupport.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using TechSupport.Context;
using Microsoft.Win32;
using TechSupport.Views;
using System.Diagnostics;

namespace TechSupport.ViewModels
{
    public class TicketsViewModel : BaseViewModel
    {
        private readonly TicketService _ticketService;
        private readonly AuthService _authService;
        private readonly IPdfService _pdfService;
        private readonly AppDbContext _context;

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
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        private string _clientNameSearch;
        public string ClientNameSearch
        {
            get => _clientNameSearch;
            set => SetProperty(ref _clientNameSearch, value);
        }

        private string _subjectSearch;
        public string SubjectSearch
        {
            get => _subjectSearch;
            set => SetProperty(ref _subjectSearch, value);
        }
        private DateTime? _dateFrom;
        public DateTime? DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }
        private DateTime? _dateTo;
        public DateTime? DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }
        private DateFilterType _selectedDateFilterType;
        public DateFilterType SelectedDateFilterType
        {
            get => _selectedDateFilterType;
            set => SetProperty(ref _selectedDateFilterType, value);
        }
        public ObservableCollection<DateFilterType> DateFilterTypes { get; } =
        new ObservableCollection<DateFilterType>
        {
            DateFilterType.CreateDate,
            DateFilterType.LastUpdateDate
        };


        public ObservableCollection<string> Statuses { get; }
        public ObservableCollection<Ticket> Tickets { get; }
        public ICommand LoadTicketsCommand { get; }
        public ICommand GeneratePdfCommand { get; }

        public ICommand FilterCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand OpenEditCommand { get; }

        public ICommand DoubleClickCommand { get; }

        public ICommand EditTicketCommand { get; }
        public ICommand ResetSearchCommand { get; }

        public int? AssignedUserId { get; set; }


        public bool IsLoading { get; private set; }


        public TicketsViewModel(TicketService ticketService, AuthService authService,IPdfService pdfService)
        {
            _ticketService = ticketService;
            _authService = authService;

            GeneratePdfCommand = new RelayCommand(async () => await GeneratePdfAsync());
            SearchCommand = new RelayCommand(async () => await SearchTicketsAsync());
            ResetSearchCommand = new RelayCommand(ResetSearch);
            Statuses = new ObservableCollection<string> { "Все", "Новая", "В работе", "Решена", "Закрыта" };
            Tickets = new ObservableCollection<Ticket>();
            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTo = DateTime.Now.Date;
            _pdfService = pdfService ?? throw new ArgumentNullException(nameof(pdfService));
            EditTicketCommand = new RelayCommand(EditSelectedTicket);
            OpenEditCommand = new RelayCommand(OpenEditTicket, CanOpenEdit);
            DoubleClickCommand = new RelayCommand(OpenEditTicket);

            FilterCommand = new RelayCommandAsync(LoadTicketsAsync); // Используем асинхронную команду
            LoadTicketsAsync();
        }
        private bool CanOpenEdit()
        {
            return SelectedTicket != null && _authService.CurrentUser != null;
        }
        private void OpenEditTicket()
        {
            try
            {
                if (!CanOpenEdit()) return;

                // Создаем копию заявки для безопасного редактирования
                var ticketCopy = CloneTicket(SelectedTicket);

                var editVM = new EditTicketViewModel(_ticketService, ticketCopy, _authService.CurrentUser)
                {
                    OnSaveCallback = (updatedTicket) =>
                    {
                        // Обновляем оригинальную заявку после сохранения
                        _context.Entry(SelectedTicket).CurrentValues.SetValues(updatedTicket);
                        LoadTickets();
                    }
                };

                var window = new EditTicketWindow(editVM)
                {
                    Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                window.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка открытия редактора: {ex}");
                NotificationService.ShowError($"Не удалось открыть редактор: {ex.Message}");
            }
        }
        private Ticket CloneTicket(Ticket original)
        {
            return new Ticket
            {
                Id = original.Id,
                Subject = original.Subject,
                Description = original.Description,
                Status = original.Status,
                // ... другие свойства ...
            };
        }


        private void EditSelectedTicket()
        {

            

            if (SelectedTicket == null)
            {
                NotificationService.ShowError("Не выбрана заявка для редактирования");
                return;
            }

            if (_authService.CurrentUser == null)
            {
                NotificationService.ShowError("Пользователь не авторизован");
                return;
            }

            if (!SelectedTicket.CanUserEdit(_authService.CurrentUser))
            {
                NotificationService.ShowError("Только ответственный может редактировать эту заявку");
                return;
            }

            try
            {
                var editWindow = new EditTicketWindow(
                    new EditTicketViewModel(_ticketService, SelectedTicket, _authService.CurrentUser));

                editWindow.Owner = Application.Current.MainWindow;
                editWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                editWindow.ShowDialog();

                LoadTickets(); // Обновляем список после закрытия окна
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при открытии окна редактирования: {ex}");
                NotificationService.ShowError($"Ошибка при открытии окна: {ex.Message}");
            }
        }

        private async Task SearchTicketsAsync()
        {
            try
            {
                var filter = new TicketFilter
                {
                    Status = SelectedStatus == "Все" ? null : SelectedStatus,
                    AssignedUserId = _authService.IsTechSupport ? _authService.CurrentUser?.Id : null,
                    SearchText = SearchText,
                    ClientNameSearch = ClientNameSearch,
                    SubjectSearch = SubjectSearch,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                };

                var tickets = await _ticketService.SearchTicketsAsync(filter);

                Tickets.Clear();
                foreach (var ticket in tickets)
                {
                    Tickets.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }
        private async Task GeneratePdfAsync()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"Заявки_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    await _pdfService.GenerateTicketsReportAsync(Tickets.ToList(), saveFileDialog.FileName);
                    MessageBox.Show("PDF отчет успешно создан!", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ResetSearch()
        {
            SearchText = string.Empty;
            ClientNameSearch = string.Empty;
            SubjectSearch = string.Empty;
            SelectedStatus = "Все";
            DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTo = DateTime.Now.Date;
            SelectedDateFilterType = DateFilterType.CreateDate;
            LoadTicketsAsync().ConfigureAwait(false);
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


        public string AssignedUserName
        {
            get
            {
                if (AssignedUserId.HasValue)
                {
                    using var context = new AppDbContext();
                    var user = context.Users.Find(AssignedUserId.Value);
                    return user?.Name ?? "Не назначен";
                }
                return "Не назначен";
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

                var tickets = await _ticketService.GetFilteredTicketsWithUsersAsync(filter); // Измените метод сервиса

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
