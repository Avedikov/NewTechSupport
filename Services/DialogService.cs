using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSupport.Views;

namespace TechSupport.Services
{
    public class DialogService : IDialogService
    {
        private readonly AuthService _authService;

        public DialogService(AuthService authService)
        {
            _authService = authService;
        }

        public void ShowLoginWindow()
        {
            // Создаем экземпляр LoginWindow именно из TechSupport.Views
            var loginWindow = new LoginWindow(_authService);
            loginWindow.Show();  // Теперь этот вызов должен работать без ошибок
        }

        public void ShowMainWindow()
        {
            throw new NotImplementedException();
        }
    }
}
