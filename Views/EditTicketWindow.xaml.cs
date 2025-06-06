using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechSupport.ViewModels;

namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для EditTicketWindow.xaml
    /// </summary>
    public partial class EditTicketWindow : Window
    {
        public EditTicketWindow(EditTicketViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
