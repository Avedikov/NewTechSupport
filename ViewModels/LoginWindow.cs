using TechSupport.Context;
using TechSupport.Views;

namespace TechSupport.ViewModels
{
    public class LoginWindow
    {
        private Services.AuthService _authService;
        private AppDbContext appDbContext;

        public LoginWindow(Services.AuthService authService)
        {
            
            _authService = authService;
        }

        public LoginWindow DataContext { get; private set; }

        
    }
}