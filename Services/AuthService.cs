using TechSupport.Context;
using TechSupport.Models;
using System.Diagnostics;

namespace TechSupport.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private User _currentUser;

        public event Action AuthenticationChanged;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                AuthenticationChanged?.Invoke();
            }
        }

        public bool IsAuthenticated => CurrentUser != null;

        // Единственное определение IsAdmin
        public bool IsAdmin => CurrentUser?.Role == "Admin";

        public bool IsTechSupport => IsAuthenticated && CurrentUser.Role == "TechSupport";

        public bool Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return false;

            CurrentUser = user;
            AuthenticationChanged?.Invoke();
            return true;
        }

        public void Logout()
        {
            Debug.WriteLine("Выполняется выход из системы");

            // 1. Очищаем текущего пользователя (вызовет AuthenticationChanged)
            CurrentUser = null;

            // 2. Дополнительные действия при выходе
            ClearSessionData();
        }

        private void ClearSessionData()
        {
            // Очистка cookies/токенов, если используются
            Debug.WriteLine("Данные сессии очищены");
        }

        public void Register(string username, string password, string name, string email)
        {
            if (_context.Users.Any(u => u.Username == username))
                throw new Exception("Пользователь с таким логином уже существует");

            var user = new User
            {
                Username = username,
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }


    }
}
