using TechSupport.Services;
using TechSupport.Views;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TechSupport.Context;
using Microsoft.Extensions.DependencyInjection;

namespace TechSupport.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private object _currentPage;
        private AppDbContext _context;

        public AuthService AuthService { get; set; }

        public object CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        // Теперь свойства получают данные из экземпляра AuthService
        public bool IsAdmin => _authService.CurrentUser?.IsAdmin ?? true;
        public bool IsTechSupport => _authService.CurrentUser?.IsTechSupport ?? true;

        public ICommand NavigateToNewTicketCommand { get; }
        public ICommand NavigateToTicketsCommand { get; }
        public ICommand NavigateToKnowledgeBaseCommand { get; }
        public ICommand NavigateToAdminCommand { get; }
        public ICommand LogoutCommand { get; private set; }


        // Внедряем AuthService через конструктор
        public MainViewModel(AuthService authService)
        {
            _authService = authService
            ?? throw new ArgumentNullException(nameof(authService));

            InitializeCommands();

            NavigateToNewTicketCommand = new RelayCommand(NavigateToNewTicket);
            NavigateToTicketsCommand = new RelayCommand(NavigateToTickets);
            NavigateToKnowledgeBaseCommand = new RelayCommand(NavigateToKnowledgeBase);
            NavigateToAdminCommand = new RelayCommand(NavigateToAdmin, () => IsAdmin);
            LogoutCommand = new RelayCommand(Logout);
        }

        private void OnAuthenticationChanged()
        {
            Console.WriteLine($"Пользователь: {_authService.CurrentUser?.Username}, Роль: {_authService.CurrentUser?.Role}");
            OnPropertyChanged(nameof(IsAdmin));
        }
        private void InitializeCommands()
        {
            LogoutCommand = new RelayCommand(Logout);
        }

        public MainViewModel(AuthService authService, object value1, object value2) : this(authService)
        {
        }

        public MainViewModel(AuthService authService, TicketService ticketService) : this(authService)
        {
        }

        private void NavigateToNewTicket()
        {
            var newTicketPage = new NewTicketPage();
            newTicketPage.DataContext = ((App)Application.Current).ServiceProvider
                .GetRequiredService<NewTicketViewModel>();
            CurrentPage = newTicketPage;
        }

        private void NavigateToTickets()
        {
            CurrentPage = new TicketsPage();
        }

        private void NavigateToKnowledgeBase()
        {
            CurrentPage = new KnowledgeBasePage();
        }

       

        private void Logout()
        {
            Console.WriteLine("Logout called");
            _authService.Logout();
            OnPropertyChanged(nameof(IsTechSupport));
            OnPropertyChanged(nameof(IsAdmin));
        }

        
        private void NavigateToAdmin()
        {

            //if (!_authService.IsAdmin)
            //{
            //    MessageBox.Show("Доступ запрещён!");
            //    return;
            //}

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Navigate(new AdminPage());
        }


    }
}
