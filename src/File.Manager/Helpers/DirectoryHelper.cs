using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.IO;

namespace File.Manager
{
    /// <summary>
    /// A helper class for logical drive
    /// </summary>
    public static class DirectoryHelper
    {
        #region Bytes Converter

        /// <summary>
        /// Converts byte value to gigabyte (GB)
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="decimalPlace">The desired amount of decimal place the returned value can have</param>
        /// <param name="getJustTheValue">True if just the numerical value is to be returned, otherwise false</param>
        /// <returns>A converted gigabyte value</returns>
        public static string ConvertByteToGigaByte(long value, int decimalPlace, bool getJustTheValue = false)
        {
            // If just the value is needed...
            if (getJustTheValue)
                // Return just the value
                return string.Format($"{Math.Round(value / (double)(1024 * 1024 * 1024), decimalPlace)}");
            // Otherwise...
            else
                // Add tag to the returned value
                return string.Format($"{Math.Round(value / (double)(1024 * 1024 * 1024), decimalPlace)} GB");
        }

        /// <summary>
        /// Converts byte value to megabyte (MB)
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="decimalPlace">The desired amount of decimal place the returned value can have</param>
        /// <param name="getJustTheValue">True if just the numerical value is to be returned, otherwise false</param>
        /// <returns>A converted megabyte value</returns>
        public static string ConvertByteToMegaByte(long value, int decimalPlace, bool getJustTheValue = false)
        {
            // If just the value is needed...
            if (getJustTheValue)
                // Return just the value
                return string.Format($"{Math.Round(value / (double)(1024 * 1024), decimalPlace)}");
            // Otherwise...
            else
                // Add tag to the returned value
                return string.Format($"{Math.Round(value / (double)(1024 * 1024), decimalPlace)} MB");
        }

        /// <summary>
        /// Converts byte value to kilobyte (GB)
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="decimalPlace">The desired amount of decimal place the returned value can have</param>
        /// <param name="getJustTheValue">True if just the numerical value is to be returned, otherwise false</param>
        /// <returns>A converted kilobyte value</returns>
        public static string ConvertByteToKiloByte(long value, int decimalPlace, bool getJustTheValue = false)
        {
            // If just the value is needed...
            if (getJustTheValue)
                // Return just the value
                return string.Format($"{Math.Round(value / (double)1024, decimalPlace)}");
            // Otherwise...
            else
                // Add tag to the returned value
                return string.Format($"{Math.Round(value / (double)1024, decimalPlace)} KB");
        }

        /// <summary>
        /// Converts raw byte value to Kilobyte, Megabyte or Gigabyte
        /// TODO: Implement converting to terabyte
        /// </summary>
        /// <param name="value">The raw value in bytes to convert</param>
        /// <param name="decimalPlace">The number of decimal places desired</param>
        /// <param name="getJustTheValue">
        /// If true, prefix [ KB | MB | GB ] to the returned converted value,
        /// Otherwise false if only the numerical value is to be returned
        /// </param>
        /// <returns></returns>
        public static string ConvertByteToReadableValue(long value, int decimalPlace, bool getJustTheValue = false)
        {
            // If total drive size is at least 1GB
            if (value >= (1 << 30))
                return ConvertByteToGigaByte(value, decimalPlace, getJustTheValue);
            // If total drive size is at least 1MB
            else if (value >= (1 << 20) && value < (1 << 30))
                return ConvertByteToMegaByte(value, decimalPlace, getJustTheValue);
            else
                // Otherwise, convert value to kilobyte
                return ConvertByteToKiloByte(value, decimalPlace, getJustTheValue);
        }

        /// <summary>
        /// Converts Readable values in Kilobyte, Megabyte or Gigabyte into raw bytes
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>Bytes size of the converted value</returns>
        public static long ConvertValueToByte(string value)
        {
            // Define variables and signatures
            string gigaByteSignNature = "GB";
            string megaByteSignNature = "MB";
            string kiloByteSignNature = "KB";
            double valueParser;
            long rawDataSize = 0L;

            // If value to be converted is GB...
            if(value.Contains(gigaByteSignNature))
            {
                // Format and convert value to double
                valueParser = double.Parse(value.Split(' ')[0]);
                // Convert value to bytes
                rawDataSize = (long)(valueParser * (1024 * 1024 * 1024));
            }

            // If value to be converted is MB...
            if (value.Contains(megaByteSignNature))
            {
                // Format and convert value to double
                valueParser = double.Parse(value.Split(' ')[0]);
                // Convert value to bytes
                rawDataSize = (long)(valueParser * (1024 * 1024));
            }

            // If value to be converted is KB...
            if (value.Contains(kiloByteSignNature))
            {
                // Format and convert value to double
                valueParser = double.Parse(value.Split(' ')[0]);
                // Convert value to bytes
                rawDataSize = (long)(valueParser * 1024);
            }

            // Return converted value 
            return rawDataSize;
        }

        #endregion

        /// <summary>
        /// Get logical drive label to include to drive letter
        /// </summary>
        /// <param name="drive">The drive to format it's label</param>
        /// <returns>Formatted drive label</returns>
        public static string GetLogicalDriveVolumeLabel(DriveInfo drive)
        {
            // Create an empty string variable
            string driveTrimmedName = string.Empty;
            // If drive is available...
            if (drive.IsReady)
                // Trim back-slash off of its name
                driveTrimmedName = drive.Name.Trim('\\');
            // return formatted drive label attached with trimmed drive name.
            return string.Format($"{drive.VolumeLabel} ({driveTrimmedName})");
        }

        /// <summary>
        /// Scans and retrieves total size of the specified path contents
        /// </summary>
        /// <param name="secondaryPath ">The secondary directory path to obtain total size of it's contents</param>
        /// <param name="path">The directory path to obtain total size of it's contents</param>
        /// <returns>Total size of a directory</returns>
        public static async Task<string> GetDirectorySizeAsync(string path, string? secondaryPath = null) 
        {
            // Set up the directory path
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            // Create total size variable
            long totalSize = 0L;

            // Run on thread
            await Task.Run(() => 
            {
                try
                {
                    // Get the total size of files in the directory path
                    totalSize = directoryInfo.EnumerateFiles("*.*", new EnumerationOptions { RecurseSubdirectories = true }).Sum(file => file.Length);
                }
                catch (Exception /* ex */ )
                {
                    // Notify developers
                    //Logger.Log(ex.Message);
                }
            });

            // If we have secondary path...
            if (!string.IsNullOrEmpty(secondaryPath))
                // Get the total size of files in the directory path
                totalSize =+ await Task.Run(() => directoryInfo.EnumerateFiles("*.*", new EnumerationOptions { RecurseSubdirectories = true }).Sum(file => file.Length));

            // Convert to appropriate format and return total size
            return ConvertByteToReadableValue(totalSize, 2);
        }

        /// <summary>
        /// Scans and retrieves total size of installed apps in 32 and 64 bit registry
        /// </summary>
        /// <returns>Total size of installed apps in 32 and 64 bit registry</returns>
        public static async Task<string> GetInstalledAppsTotalSizeAsync() 
        {
            // Create total size variable
            long totalSize = 0;

            // Registry keys to check for installed applications
            string[] registryKeys =
            {
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall",
            };

            // Run on thread
            await Task.Run(() =>
            {
                // Loop through both 32-bit and 64-bit registry
                foreach (var keyPath in registryKeys)
                {
                    using (var key = Registry.LocalMachine.OpenSubKey(keyPath))
                    {
                        if (key != null)
                        {
                            foreach (var subKeyName in key.GetSubKeyNames())
                            {
                                using (var subkey = key.OpenSubKey(subKeyName))
                                {
                                    var sizeObj = subkey?.GetValue("EstimatedSize");
                                    if (sizeObj != null)
                                    {
                                        long size = Convert.ToInt64(sizeObj) * 1024;
                                        totalSize += size;
                                    }
                                }
                            }
                        }
                    }
                }

            });

            // Convert to appropriate format and return total size
            return ConvertByteToReadableValue(totalSize, 2);
        }

        /// <summary>
        /// Resolve shortcut to a full path
        /// </summary>
        /// <param name="shortcut">The shortcut to resolve</param>
        /// <returns>Resolved full path</returns>
        public static string ResolveShortcut(string shortcut)
        {
            // Create an empty resolved full path
            string resolvedFullPath = string.Empty;

            // Create window script host
            WshShell shell = new WshShell();

            try
            {
                // Attempt to get path out of the passed in shortcut
                resolvedFullPath = ((IWshShortcut)shell.CreateShortcut(shortcut)).TargetPath;
            }
            // Handle exceptions that might occur
            catch (Exception /* ex */)
            {
                // Notify developers
            }

            // Return resolved full path
            return resolvedFullPath;
        }
    }
}
