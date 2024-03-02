using System.Collections.ObjectModel;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="SideMenuControl"/> of this application
    /// </summary>
    public class SideMenuControlViewModel : ViewModelBase
    {
        #region Private Properties

        /// <summary>
        /// Overview items
        /// </summary>
        private DirectoryCollectionControlViewModel _overViewItems;

        /// <summary>
        /// QuickAccess items
        /// </summary>
        private DirectoryCollectionControlViewModel _quickAccessItems;

        /// <summary>
        /// Main library items
        /// </summary>
        private DirectoryCollectionControlViewModel _mainLibraryItems;

        /// <summary>
        /// Drives items
        /// </summary>
        private DirectoryCollectionControlViewModel _drivesItems;

        #endregion

        #region Public Properties

        /// <summary>
        /// Overview items
        /// </summary>
        public DirectoryCollectionControlViewModel OverViewItems 
        {
            // Get items
            get => _overViewItems;
            set
            {
                // If values aren't the same...
                if (_overViewItems != value)
                    // Make value the same
                    _overViewItems = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// QuickAccess items
        /// </summary>
        public DirectoryCollectionControlViewModel QuickAccessItems
        {
            // Get items
            get => _quickAccessItems;
            set
            {
                // If values aren't the same...
                if (_quickAccessItems != value)
                    // Make value the same
                    _quickAccessItems = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Main library items
        /// </summary>
        public DirectoryCollectionControlViewModel MainLibraryItems
        {
            // Get items
            get => _mainLibraryItems;
            set
            {
                // If values aren't the same...
                if (_mainLibraryItems != value)
                    // Make value the same
                    _mainLibraryItems = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Drives items
        /// </summary>
        public DirectoryCollectionControlViewModel DrivesItems
        {
            // Get items
            get => _drivesItems;
            set
            {
                // If values aren't the same...
                if (_drivesItems != value)
                    // Make value the same
                    _drivesItems = value;
                // Update property
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuControlViewModel()
        {
            // Create required objects
            var selectionEvent = new SelectionChangedEvent();

            #region Properties defaults

            // Set defaults for each item
            // Overview
            _overViewItems =  new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Home,
                        DirectoryName = "Home", 
                        IsSelected = true,
                    },
                }
            };
            // Quick access
            _quickAccessItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Github",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Freelance",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Embedded System Design",
                    },
                }
            };
            // Main library
            _mainLibraryItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Desktop",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Documents",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Downloads,
                        DirectoryName = "Downloads",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Pictures,
                        DirectoryName = "Pictures",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Videos,
                        DirectoryName = "Videos",
                    },
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Music,
                        DirectoryName = "Music",
                    },

                }
            };
            // Drives
            _drivesItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Drives,
                        DirectoryName = "Local Disk (C:)",
                    },
                }
            };

            #endregion
        }

    }
}
