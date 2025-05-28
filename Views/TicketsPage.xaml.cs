using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using TechSupport.ViewModels;

namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        public TicketsPage()
        {
            InitializeComponent();

            DataContext = ((App)Application.Current).ServiceProvider
                    .GetRequiredService<TicketsViewModel>();
        }

        
    }
}
