using System.Globalization;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// Design-time data for <see cref="MainWindow"/> content
    /// </summary>
    internal class MainWindowContentDesignTimeData : IValueConverter
    {
        /// <summary>
        /// A single instance of this class
        /// </summary>
        public static MainWindowContentDesignTimeData DesignInstance => new MainWindowContentDesignTimeData();

        /// <summary>
        /// Design-time view model
        /// </summary>
        public static ApplicationViewModel DesignTimeAppViewModel => new ApplicationViewModel(new NavigationService());

        /// <summary>
        /// Return <see cref="HomePage"/> as the converted value
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new HomePage();

        /// <summary>
        /// CONVERT BACK NOT IN-USE OR IMPLEMENTED
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
