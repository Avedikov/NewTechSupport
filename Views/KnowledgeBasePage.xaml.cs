using TechSupport.Context;
using System.Windows;
using System.Windows.Controls;
using TechSupport.ViewModels;
using TechSupport.Models;
using TechSupport.Services;


namespace TechSupport.Views
{
    /// <summary>
    /// Логика взаимодействия для KnowledgeBasePage.xaml
    /// </summary>
    public partial class KnowledgeBasePage : Page
    {
        private readonly KnowledgeBaseViewModel _viewModel;

        public KnowledgeBasePage()
        {
            InitializeComponent();

            // Получаем сервисы через DI или создаем новые
            var context = new AppDbContext();
            var authService = new AuthService(context);

            _viewModel = new KnowledgeBaseViewModel(context, authService);
            DataContext = _viewModel;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadArticles();
        }
    }
}
