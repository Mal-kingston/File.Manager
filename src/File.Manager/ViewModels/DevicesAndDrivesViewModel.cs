﻿using IWshRuntimeLibrary;
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
                    _devicesAndDrives.Add(new DriveItemControlViewModel()
                    {
                        IconType = drive.DriveType.Equals(DriveType.Fixed) ? IconType.WindowsLogo : IconType.Drives,
                        DriveName = $"{drive.VolumeLabel} ({drive.Name.Trim('\\')})",
                        TotalDriveSizeMaxRange = DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize, 2, getJustTheValue: true),
                        DriveCurrentValue = DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize - drive.AvailableFreeSpace, 2, getJustTheValue: true),
                        TotalDriveSize = $"{DirectoryHelper.ConvertByteToReadableValue(drive.AvailableFreeSpace, 2)}  available  of  {DirectoryHelper.ConvertByteToReadableValue(drive.TotalSize, 2)}",
                        FullPath = drive.RootDirectory.FullName,
                    });
                }
            }

            // Fake drive item
            _devicesAndDrives.Add(new DriveItemControlViewModel()
            {
                IconType = IconType.Drives,
                DriveName = "fake-drive",
                TotalDriveSizeMaxRange = DirectoryHelper.ConvertByteToReadableValue(500000000000L, 2, getJustTheValue: true),
                DriveCurrentValue = DirectoryHelper.ConvertByteToReadableValue(500000000000L - 200000000000L, 2, getJustTheValue: true),
                TotalDriveSize = $"{DirectoryHelper.ConvertByteToReadableValue(200000000000L, 2)}  available  of  {DirectoryHelper.ConvertByteToReadableValue(500000000000L, 2)}",
                FullPath = "C:\\"
            });
        }
    }
}
