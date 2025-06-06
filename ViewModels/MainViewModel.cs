using TechSupport.Services;
using TechSupport.Views;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TechSupport.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TechSupport.Models;

namespace TechSupport.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private object _currentPage;
        private AppDbContext _context;
        private User _currentUser;

        public event Action AuthenticationChanged;

        public AuthService AuthService { get; set; }

        public object CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        // Теперь свойства получают данные из экземпляра AuthService
        public bool IsAdmin => _authService.CurrentUser?.IsAdmin ?? true;
        public bool IsTechSupport => _authService.CurrentUser?.IsTechSupport ?? false;

        public ICommand NavigateToNewTicketCommand { get; }
        public ICommand NavigateToTicketsCommand { get; }
        public ICommand NavigateToKnowledgeBaseCommand { get; }
        public ICommand NavigateToAdminCommand { get; }
        public ICommand LogoutCommand { get; private set; }
        public object CurrentUser { get; private set; }


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
            LogoutCommand = new RelayCommand(ExecuteLogout);

        }
        private void ExecuteLogout()
        {
            Debug.WriteLine("Команда выхода выполнена");
            _authService.Logout();

            // Дополнительные действия после выхода
            NavigateToLogin();
        }

        private void NavigateToLogin()
        {
            // Переход на страницу входа
            Application.Current.Dispatcher.Invoke(() =>
            {
                var loginWindow = new Views.LoginWindow();
                loginWindow.Show();

                // Закрываем текущее окно
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow) window.Close();
                }
            });
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
            Debug.WriteLine("[1] Начало выхода");
            CurrentUser = null;
            Debug.WriteLine("[2] Пользователь очищен");
            AuthenticationChanged?.Invoke();
            Debug.WriteLine("[3] Событие вызвано");
        }

        
        private void NavigateToAdmin()
        {

            if (!_authService.IsAdmin)
            {
                MessageBox.Show("Доступ запрещён!");
                return;
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainFrame.Navigate(new AdminPage());
        }


    }
}
