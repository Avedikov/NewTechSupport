using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TechSupport.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string status) return Brushes.Black;

            return status.ToLower() switch
            {
                "новая" => Brushes.Blue,
                "в работе" => Brushes.Orange,
                "решена" => Brushes.Green,
                "закрыта" => Brushes.Gray,
                _ => Brushes.Black
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
