using System.Windows;
using TechSupport.ViewModels;

namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly Services.AuthService _authService;
        public RegisterWindow(RegisterViewModel viewModel)
        {
            InitializeComponent();
            
            DataContext = viewModel;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}
