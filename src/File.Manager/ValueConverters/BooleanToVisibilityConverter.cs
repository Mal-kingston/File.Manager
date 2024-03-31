using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// Takes a boolean and convert it to <see cref="Visibility"/>
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Single instance of this class to bind to in xaml
        /// </summary>
        public readonly static BooleanToVisibilityConverter Instance = new BooleanToVisibilityConverter();

        /// <summary>
        /// Converts an object from one type to another
        /// </summary>
        /// <param name="value">The object to convert</param>
        /// <returns>A visibility based of value of a boolean</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If value is true, return visible, otherwise return collapsed
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
