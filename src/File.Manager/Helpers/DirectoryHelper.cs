using IWshRuntimeLibrary;
using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private static string ConvertByteToGigaByte(long value, int decimalPlace, bool getJustTheValue = false)
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
        private static string ConvertByteToMegaByte(long value, int decimalPlace, bool getJustTheValue = false)
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
        private static string ConvertByteToKiloByte(long value, int decimalPlace, bool getJustTheValue = false)
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

        /// <summary>
        /// Types of common files
        /// </summary>
        const string PNGFile = "png file";
        const string JPGFile = "jpg file";
        const string MP4File = "mp4 file";
        const string MP3File = "mp3 file";

        /// <summary>
        /// Gets a file icon based on it's type
        /// </summary>
        /// <param name="fileType">The type of file to use to in finding the appropriate icon</param>
        /// <returns>Type of icon <see cref="IconType"/></returns>
        public static IconType GetFileIconType(string fileType)
        {
            // Sort and retrieve the appropriate icon
            switch (fileType.ToLower())
            {
                // Images / Pictures
                case PNGFile:
                case JPGFile:
                    return IconType.Pictures;

                // Videos
                case MP4File:
                    return IconType.Videos;

                // Audios
                case MP3File:
                    return IconType.Music;

                // Default to document icon type
                default: 
                    return IconType.Documents;
            }
        }

        #region DLL import

        //Desktop: {B4BFCC3A-DB2C-424C-B029-7FE99A87C641}
        //Documents: {FDD39AD0-238F-46AF-ADB4-6C85480369C7}
        //Pictures: {33E28130-4E1E-4676-835A-98395C3BC3BB}
        //Music: {4BD8D571-6D19-48D3-BE97-422220080E43}
        //Videos: {18989B1D-99B5-455B-841C-AB7C74E4DDFC}
        //Downloads: {374DE290-123F-4565-9164-39C4925E467B}

        // Default directories id
        public static readonly Guid Desktop = new Guid("B4BFCC3A-DB2C-424C-B029-7FE99A87C641");
        public static readonly Guid Documents = new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7");
        public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        public static readonly Guid Music = new Guid("4BD8D571-6D19-48D3-BE97-422220080E43");
        public static readonly Guid Pictures = new Guid("33E28130-4E1E-4676-835A-98395C3BC3BB");
        public static readonly Guid Videos = new Guid("18989B1D-99B5-455B-841C-AB7C74E4DDFC");
        public static readonly Guid OneDrive = new Guid("A52BBA46-E9E1-435f-B3D9-28DAA648C0F6");

        /// <summary>
        /// DLL Import
        /// </summary>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

        /// <summary>
        /// Gets full path to the specified default directory type
        /// </summary>
        /// <param name="defaultDirectoryType">The <see cref="DefaultDirectoryType"/></param>
        /// <returns>Full path to a specified default path</returns>
        /// <exception cref="NotImplementedException">Thrown when path provided hasn't been implemented</exception>
        public static string GetDefaultDirectoryPath(DefaultDirectoryType defaultDirectoryType)
        {
            // Sort and get the appropriate path
            switch(defaultDirectoryType)
            {
                case DefaultDirectoryType.Desktop:
                    return GetDirectoryPath(Desktop);

                case DefaultDirectoryType.Documents:
                    return GetDirectoryPath(Documents);

                case DefaultDirectoryType.Downloads:
                    return GetDirectoryPath(Downloads);

                case DefaultDirectoryType.Music:
                    return GetDirectoryPath(Music);

                case DefaultDirectoryType.Pictures:
                    return GetDirectoryPath(Pictures);

                case DefaultDirectoryType.Videos:
                    return GetDirectoryPath(Videos);

                case DefaultDirectoryType.OneDrive:
                    return GetDirectoryPath(OneDrive);
                
                // Throw exception if path isn't implemented
                default:
                    throw new NotImplementedException("Directory not set up yet");
            }

        }

        /// <summary>
        /// Get known directory path
        /// </summary>
        /// <param name="defaultPathId">The id of the know directory</param>
        /// <returns>Full path of a known directory</returns>
        /// <exception cref="Exception"></exception>
        private static string GetDirectoryPath(Guid defaultPathId)
        {
            IntPtr pszPath;
            SHGetKnownFolderPath(defaultPathId, 0, IntPtr.Zero, out pszPath);
            string? path = Marshal.PtrToStringUni(pszPath);
            Marshal.FreeCoTaskMem(pszPath);

            // Make sure path is not null
            if (path == null)
                throw new Exception("Invalid path");

            // Return path
            return path;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public static class FILE_ATTRIBUTE
        {
            public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
        }

        public static class SHGFI
        {
            public const uint SHGFI_TYPENAME = 0x000000400;
            public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        /// <summary>
        /// Fetches type of file from the specified file path
        /// </summary>
        /// <param name="filePath">The specified file path</param>
        /// <returns>Type of file</returns>
        public static string GetFileType(string filePath)
        {

            SHFILEINFO info = new SHFILEINFO();

            uint dwFileAttributes = FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
            uint uFlags = (uint)(SHGFI.SHGFI_TYPENAME | SHGFI.SHGFI_USEFILEATTRIBUTES);

            SHGetFileInfo(filePath, dwFileAttributes, ref info, (uint)Marshal.SizeOf(info), uFlags);

            // Return file type
            return info.szTypeName;

        }


        #endregion

    }
}
