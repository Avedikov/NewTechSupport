using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TechSupport.Models;
using TechSupport.Context;
using TechSupport.Services;
using TechSupport.Views;

namespace TechSupport.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword &&
                   Password.Length >= 6;
        }

        private void Register()
        {
            try
            {
                _authService.Register(Username, Password, Name, Email);
                MessageBox.Show("Регистрация успешна!");
                CloseWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            }
        }

        private void CloseWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (Window window in Application.Current.Windows)
                    if (window is RegisterWindow) window.Close();
            });
        }
    }
}
