using System.ComponentModel;
using System.Windows.Input;
using TechSupport.ViewModels;

namespace TechSupport.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged // Наследуем интерфейс
    {
        public event PropertyChangedEventHandler PropertyChanged; // Событие

        // Метод для вызова события
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Пример свойства с уведомлением
        private string _userName;
        private Services.AuthService authService;
        private IServiceProvider serviceProvider;

        private readonly Services.AuthService _authService;



        public LoginViewModel(Services.AuthService authService)
        {
            _authService = authService;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName)); // Теперь ошибки не будет
            }
        }
    }
}