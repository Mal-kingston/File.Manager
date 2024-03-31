using File.Manager;
using System.Globalization;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// An icon converter
    /// </summary>
    public class IconTypeConverter : IValueConverter
    {
        /// <summary>
        /// A singleton static instance of this application.
        /// </summary>
        public static IconTypeConverter Instance => new IconTypeConverter();

        /// <summary>
        /// Converts a fontawesome icon defined in XAML to unicode string
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Unicode string of a fontawesome</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return desired icon type
            return((IconType)value).GetFontAwesomeIcon();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
