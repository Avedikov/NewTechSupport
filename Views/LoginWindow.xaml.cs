using System.Windows;
using System.Windows.Controls;
using TechSupport;
using TechSupport.Views;
using TechSupport.ViewModels;
using AvedikovDiplom;
using Microsoft.Extensions.DependencyInjection;

namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly Services.AuthService _authService;

        private readonly IServiceProvider _serviceProvider;

        public LoginWindow(Services.AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        public LoginWindow()
        {
        }

        public new object DataContext
        {
            get => base.DataContext;
            set => base.DataContext = value; // public set
        }

       

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. Проверка на null для сервиса и элементов управления
            if (_authService == null)
            {
                MessageBox.Show("Ошибка: сервис аутентификации не инициализирован.");
                return;
            }

            if (txtUsername == null || txtPassword == null)
            {
                MessageBox.Show("Ошибка: элементы управления не найдены.");
                return;
            }

            // 2. Проверка введенных данных
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            // 3. Вызов метода аутентификации
            bool isAuthenticated = _authService.Login(username, password);

            if (isAuthenticated)
            {
                // 4. Создание главного окна через View, а не ViewModel
                var mainWindow = ((App)Application.Current).ServiceProvider
               .GetRequiredService<MainWindow>();

                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }

        }


        //private void RegisterButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Логика обработки нажатия кнопки регистрации
        //    var viewModel = new RegisterViewModel(_authService); // Создаем ViewModel
        //    var registerWindow = new RegisterWindow(viewModel,

        //    // Привязка PasswordBox к ViewModel
        //    passwordBox: PasswordBox); // Передаем в конструктор
        //    registerWindow.Show();
        //}
        //public LoginWindow() : this(new AuthService()) 
        //{ 

            //}
        public new void Show() => base.Show();


    }
}
