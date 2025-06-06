using TechSupport.Context;
using TechSupport.ViewModels;
using TechSupport.Views;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TechSupport.Services;
using TechSupport.Converters;

namespace TechSupport
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Регистрация контекста БД
            services.AddDbContext<AppDbContext>();

            // Сервисы (Singleton для сохранения состояния)
            services.AddSingleton<AuthService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-2G4ET2H\\MSSQLSERVER01;Database=TechSupport;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));

            services.AddSingleton<TicketService>();

            services.AddTransient<NewTicketViewModel>();

            services.AddTransient<KnowledgeBasePage>();

            services.AddTransient<KnowledgeBaseViewModel>();

            services.AddSingleton<IPdfService, PdfService>();
            

            services.AddTransient<TicketsViewModel>();

            services.AddTransient<MainViewModel>();

            // ViewModels
            services.AddTransient<MainViewModel>(provider =>
                new MainViewModel(provider.GetRequiredService<AuthService>()));

            // Окна
            services.AddTransient<MainWindow>(provider =>
                 new MainWindow { DataContext = provider.GetRequiredService<MainViewModel>() });

            

            ServiceProvider = services.BuildServiceProvider();

            //var serviceProvider = services.BuildServiceProvider();

            // Показываем LoginWindow
            var loginWindow = new Views.LoginWindow(ServiceProvider.GetRequiredService<AuthService>());
            loginWindow.Show();
           
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация сервисов
            services.AddSingleton<Services.AuthService>();
            services.AddSingleton<ViewModels.MainViewModel>();
            services.AddTransient<Views.LoginWindow>();
            services.AddTransient<Views.EditTicketWindow>();

            // Регистрация окон
            services.AddTransient<Views.MainWindow>();
            services.AddTransient<TechSupport.Views.LoginWindow>();
            services.AddTransient<RegisterWindow>();

            // Регистрация ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<TechSupport.ViewModels.LoginViewModel>();
            services.AddTransient<TicketsViewModel>();

            services.AddSingleton<Services.AuthService>();
            services.AddTransient<Views.MainWindow>();

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=DESKTOP-2G4ET2H\\MSSQLSERVER01;Database=TechSupport;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;"));
            services.AddSingleton<Services.AuthService>();
            services.AddTransient<Views.LoginWindow>();
            services.AddSingleton<BoolToVisibilityConverter>();
            services.AddTransient<ViewModels.MainViewModel>();
            services.AddSingleton<Services.AuthService>();
            services.AddDbContext<AppDbContext>();
            services.AddSingleton<Services.AuthService>();
            services.AddTransient<ViewModels.AdminViewModel>();
            services.AddTransient<Views.AdminPage>();
            services.AddTransient<Views.KnowledgeBasePage>();
            services.AddTransient<ViewModels.NewTicketViewModel>();
            services.AddSingleton<Services.TicketService>();
            services.AddSingleton<Services.AuthService>();
            



        }
    }

}
