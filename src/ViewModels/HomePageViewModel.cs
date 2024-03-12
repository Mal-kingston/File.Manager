using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using static File.Manager.DrivesHelper;

namespace File.Manager
{
    /// <summary>
    /// View model for the <see cref="HomePage"/> of this application
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {

        /// <summary>
        /// Tabs
        /// <remark>
        /// Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        private ObservableCollection<TabItemModel> _tabs;

        /// <summary>
        /// View model for <see cref="DriveBarControl"/>
        /// </summary>
        private DriveBarControlViewModel _driveBarControlVM;

        /// <summary>
        /// Tabs
        /// <remark>
        /// Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        public ObservableCollection<TabItemModel> Tabs 
        {
            get => _tabs;
            set
            {
                // If tabs value are not the same...
                if (_tabs != value)
                    // Set tabs value 
                    _tabs = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        public DriveBarControlViewModel DriveBarControlVM
        {
            get => _driveBarControlVM;
            set
            {
                // If tabs value are not the same...
                if (_driveBarControlVM != value)
                    // Set tabs value 
                    _driveBarControlVM = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePageViewModel()
        {
            // Set field defaults
            _tabs = new ObservableCollection<TabItemModel>();
            _driveBarControlVM = new DriveBarControlViewModel();

            // Go through each drive on the machine
            foreach ( var drive in DriveInfo.GetDrives())
            {
                // Make sure drive is available
                if (drive.IsReady)
                {
                    // If total drive size is at least 1GB
                    if (drive.TotalSize > 1 << 30)
                    {
                        // Convert drive values from byte to gigabyte
                        DriveBarControlVM.TotalDriveSize = ConvertByteToGigaByte(drive.TotalSize, 2);
                        DriveBarControlVM.MaxRange = ConvertByteToGigaByte(drive.TotalSize, 2, true);
                        DriveBarControlVM.CurrentMeterValue = ConvertByteToGigaByte(drive.TotalSize - drive.AvailableFreeSpace, 2, true);
                        DriveBarControlVM.UsedSpace = ConvertByteToGigaByte(drive.TotalSize - drive.AvailableFreeSpace, 2);
                        DriveBarControlVM.UnUsedSpace = ConvertByteToGigaByte(drive.AvailableFreeSpace, 2);
                        DriveBarControlVM.UsePercentage = true;
                    }
                    // Otherwise, if total drive size is less that 1GB
                    else
                    {
                        // Convert drive values from byte to megabyte
                        DriveBarControlVM.TotalDriveSize = ConvertByteToMegaByte(drive.TotalSize, 2);
                        DriveBarControlVM.CurrentMeterValue = ConvertByteToMegaByte(drive.TotalSize - drive.AvailableFreeSpace, 2);
                        DriveBarControlVM.UsedSpace = ConvertByteToMegaByte(drive.TotalSize - drive.AvailableFreeSpace, 2);
                        DriveBarControlVM.UnUsedSpace = ConvertByteToMegaByte(drive.AvailableFreeSpace, 2);
                    }

                    // Create label for each drive 
                    var label = FormatLogicalDriveVolumeLabel(drive);
                    // Add each drive to tab item.
                    _tabs.Add(new TabItemModel { Header = label, Content = DriveBarControlVM });
                }
            }
        }
    }
}
