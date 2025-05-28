using TechSupport.Context;
using TechSupport.Models;
using TechSupport.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для NewTicketPage.xaml
    /// </summary>
    public partial class NewTicketPage : Page
    {
        private readonly NewTicketViewModel _viewModel;

        public NewTicketPage()
        {
            InitializeComponent();

            // Инициализация ViewModel
            DataContext = ((App)Application.Current).ServiceProvider
                    .GetRequiredService<NewTicketViewModel>();
        }

       

        private void CreateTicket_Click(object sender, RoutedEventArgs e)
        {
            // Вся логика создания теперь во ViewModel
            _viewModel.CreateTicketCommand.Execute(null);
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            // Вся логика прикрепления файлов во ViewModel
            _viewModel.AttachFileCommand.Execute(null);
        }
    }
}

