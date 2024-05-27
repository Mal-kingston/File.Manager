using System.Windows.Input;

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

        /// <summary>
        /// The path to this drive item 
        /// </summary>
        public string FullPath { get; set;} = string.Empty;

        /// <summary>
        /// Type of icon to use for each of this item
        /// </summary>
        public IconType IconType { get; set; }

        /// <summary>
        /// Command to open this item
        /// </summary>
        public ICommand OpenDriveCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DriveItemControlViewModel()
        {
            // Create commands
            OpenDriveCommand = new RelayCommand(OpenDrive, canExecuteCommand => true);
        }

        private void OpenDrive()
        {
            ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.DirectoryExplorer, FullPath);
        }
    }
}
