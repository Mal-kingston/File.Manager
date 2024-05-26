using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// Converter for application page title
    /// </summary>
    public class ApplicationPageTitleConverter : IValueConverter
    {
        /// <summary>
        /// Single instance of this class to bind to in xaml
        /// </summary>
        public static ApplicationPageTitleConverter Instance => new ApplicationPageTitleConverter();

        /// <summary>
        /// Converts an object from one type to another
        /// </summary>
        /// <param name="value">The object to convert</param>
        /// <returns>Page title of a page</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Sort and returns the appropriate page title
            switch((ApplicationPages)value)
            {
                // Home
                case ApplicationPages.Home:
                    return "Home";
                
                // Directory 
                case ApplicationPages.DirectoryExplorer:
                    int currentDirectoryName = ServiceLocator.NavigationBarVM.CurrentDirectoryFullPath.Count;
                    return ServiceLocator.NavigationBarVM.CurrentDirectoryFullPath[currentDirectoryName - 1].DirectoryName;

                // Devices and drives
                case ApplicationPages.DrivesAndDevices:
                    return "Storage";

                // Exception
                default:
                     throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
