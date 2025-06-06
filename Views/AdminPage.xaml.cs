using System.Windows.Controls;
using TechSupport.Context;
using TechSupport.ViewModels;
namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();

            // Создаем контекст БД и ViewModel после инициализации компонентов
            var context = new AppDbContext();
            DataContext = new AdminViewModel(context);
        }


    }
}
