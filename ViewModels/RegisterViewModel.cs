using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TechSupport.Models;
using TechSupport.Context;

namespace TechSupport.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly Services.AuthService _authService;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private readonly AppDbContext _context;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        

        public RegisterViewModel(Services.AuthService authService)
        {
            _authService = authService;

            // Используем non-generic версию RelayCommand
            RegisterCommand = new RelayCommand(Register, CanRegister);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword;
        }

        public void Register(string username, string password)
        {
            
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private void Cancel()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = false;
                    window.Close();
                    break;
                }
            }
        }
    }
}
