using System.Collections.ObjectModel;
using System.IO;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DrivesAndDevicesPage"/>
    /// </summary>
    public class DevicesAndDrivesViewModel : ViewModelBase
    {
        /// <summary>
        /// A collection of drives / storage devices on this machine
        /// </summary>
        private ObservableCollection<DriveItemControlViewModel> _devicesAndDrives;

        /// <summary>
        /// A collection of drives / storage devices on this machine
        /// </summary>
        public ObservableCollection<DriveItemControlViewModel> DevicesAndDrives
        { 
            get => _devicesAndDrives;
            set
            {
                // If value is different...
                if(_devicesAndDrives != value) 
                    // Set value 
                    _devicesAndDrives = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DevicesAndDrivesViewModel()
        {
            // Set defaults
            _devicesAndDrives = new ObservableCollection<DriveItemControlViewModel>();

            // Get drives
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                // Make sure the drive is available
                if (drive.IsReady)
                {
                    // Add drive to collection
                    _devicesAndDrives.Add(new DriveItemControlViewModel
                    {
                        DriveName = drive.DriveType.Equals(DriveType.Fixed) ? $"{drive.VolumeLabel} (C:)" : drive.VolumeLabel,
                        TotalDriveSize = DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize, 2),
                        TotalDriveSizeMaxRange = DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize, 2, getJustTheValue: true),
                        DriveCurrentValue = DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize - drive.AvailableFreeSpace, 2, getJustTheValue: true),
                    });
                }
            }

        }
    }
}
