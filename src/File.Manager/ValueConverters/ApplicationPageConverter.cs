using Microsoft.Extensions.DependencyInjection;
using System.CodeDom;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// Converts a <see cref="ApplicationPages"/> into a view
    /// </summary>
    public class ApplicationPageConverter : IValueConverter
    {
        /// <summary>
        /// Single instance of this class to bind to in xaml
        /// </summary>
        public static ApplicationPageConverter Instance => new ApplicationPageConverter();

        /// <summary>
        /// Converts an object from one type to another
        /// </summary>
        /// <param name="value">The object to convert</param>
        /// <returns>Converted application page view</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Sort and return the appropriate page
            switch ((ApplicationPages)value)
            {
                // Home page 
                case ApplicationPages.Home:
                    return DependencyInjection.GetDependency<HomePage>();

                // Drives and plugged in devices page
                case ApplicationPages.DrivesAndDevices:
                    return DependencyInjection.GetDependency<DrivesAndDevicesPage>();

                // Drives and plugged in devices page
                case ApplicationPages.DirectoryExplorer:
                    return DependencyInjection.GetDependency<DirectoryExplorer>();

                // Default
                default:
                    //return DependencyInjection.GetDependency<HomePage>();
                    throw new Exception("Application page not found");

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
