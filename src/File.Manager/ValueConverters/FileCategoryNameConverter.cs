using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace File.Manager
{
    /// <summary>
    /// A converter for file category names
    /// </summary>
    public class FileCategoryNameConverter : IValueConverter
    {
        /// <summary>
        /// Single instance of this class to bind to in xaml
        /// </summary>
        public readonly static FileCategoryNameConverter Instance = new FileCategoryNameConverter();

        /// <summary>
        /// Converts <see cref="FileType"/> name of a specific category to a formatted category name
        /// </summary>
        /// <param name="value">The type of file to convert</param>
        /// <returns>Formatted category name</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Sort and return formatted category name
            switch((FileType)value)
            {
                // Pictures / Images
                case FileType.Pictures:
                    return "Pictures";
                // Music
                case FileType.Music:
                    return "Music";
                // Videos
                case FileType.Videos:
                    return "Videos";
                // Documents
                case FileType.Documents:
                    return "Documents";
                // Installed apps
                case FileType.InstalledApps:
                    return "Installed apps";
                // Temporary files
                case FileType.TemporaryFiles:
                    return "Temporary files";
                // Empty default
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
