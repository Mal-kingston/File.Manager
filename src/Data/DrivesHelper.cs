using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace File.Manager
{
    /// <summary>
    /// A helper class for logical drive
    /// </summary>
    public static class DrivesHelper
    {
        /// <summary>
        /// Format logical drive label to include to drive letter
        /// </summary>
        /// <param name="drive">The drive to format it's label</param>
        /// <returns>Formatted drive label</returns>
        public static string FormatLogicalDriveVolumeLabel(DriveInfo drive)
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
        /// Converts byte value to gigabyte (GB)
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="decimalPlace">The desired amount of decimal place the returned value can have</param>
        /// <param name="getJustTheValue">True if just the numerical value is to be returned, otherwise false</param>
        /// <returns>A converted gigabyte value</returns>
        public static string ConvertByteToGigaByte(long value, int decimalPlace, bool getJustTheValue = false)
        {
            // If just the value is needed...
            if(getJustTheValue)
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

    }
}
