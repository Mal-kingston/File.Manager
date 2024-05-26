namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DriveItemControl"/>
    /// </summary>
    public class DriveItemControlViewModel : ViewModelBase
    {
        /// <summary>
        /// Name of drive
        /// </summary>
        public string DriveName { get; set; } = string.Empty;

        /// <summary>
        /// The current value indicating used space mark of drive
        /// </summary>
        public string DriveCurrentValue { get; set; } = string.Empty;

        /// <summary>
        /// The total size of drive
        /// </summary>
        public string TotalDriveSize { get; set; } = string.Empty;

        /// <summary>
        /// Maximum range for the drive meter
        /// </summary>
        public string TotalDriveSizeMaxRange { get; set; } = string.Empty;

    }
}
