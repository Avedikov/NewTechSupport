using TechSupport.Context;
using TechSupport.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Generators;

namespace TechSupport.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;


        private User _currentUser;
        public bool IsAuthenticated => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Role == "Admin";
        public bool IsTechSupport => IsAuthenticated && CurrentUser.Role == "TechSupport";

        public event Action AuthenticationChanged;



        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public bool Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash); // Теперь хэш корректен

            AuthenticationChanged?.Invoke();
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

        

        public void Logout()
        {
            CurrentUser = null;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Реализация проверки хэша (пример)
            using var sha256 = SHA256.Create();
            byte[] inputHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            string inputHashString = BitConverter.ToString(inputHash).Replace("-", "");
            return inputHashString.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }

        public void Register(string username, string password)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password) // Хэш включает соль автоматически
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }



        public bool Authenticate(string username, string password)
        {
            // Заглушка для примера
            return !string.IsNullOrEmpty(username) && password == "123";
        }

        internal bool Register(string username)
        {
            throw new NotImplementedException();
        }
    }
}
