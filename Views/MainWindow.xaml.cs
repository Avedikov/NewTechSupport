
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechSupport;
using TechSupport.Services;
using TechSupport.ViewModels;

namespace TechSupport.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AuthService _authService;
        private MainViewModel mainViewModel;

        public AuthService AuthService { get; }

        // Единственный конструктор
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).ServiceProvider
            .GetRequiredService<MainViewModel>();
        }

        
    }

}