using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TechSupport.Context;

namespace TechSupport.Converters
{
    public class UserIdToNameConverter : IValueConverter
    {
        private readonly AppDbContext _context;

        public UserIdToNameConverter()
        {
            _context = new AppDbContext(); // Или получить через DI
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int userId)
            {
                var user = _context.Users.Find(userId);
                return user?.Name ?? "Не назначен";
            }
            return "Не назначен";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
