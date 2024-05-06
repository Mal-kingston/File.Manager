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
        private SideMenuItemCollectionControlViewModel _overViewItems;

        /// <summary>
        /// QuickAccess items
        /// </summary>
        private SideMenuItemCollectionControlViewModel _quickAccessItems;

        /// <summary>
        /// Main library items
        /// </summary>
        private SideMenuItemCollectionControlViewModel _mainLibraryItems;

        /// <summary>
        /// Drives items
        /// </summary>
        private SideMenuItemCollectionControlViewModel _drivesItems;

        #endregion

        #region Public Properties

        /// <summary>
        /// Overview items
        /// </summary>
        public SideMenuItemCollectionControlViewModel OverViewItems 
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
        public SideMenuItemCollectionControlViewModel QuickAccessItems
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
        public SideMenuItemCollectionControlViewModel MainLibraryItems
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
        public SideMenuItemCollectionControlViewModel DrivesItems
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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuControlViewModel()
        {
            // Create required objects
            SelectionChangedEvent selectionEvent = new SelectionChangedEvent();

            #region Properties defaults

            // Set defaults for each item
            // Overview [ this item is selected by default ]
            _overViewItems =  new SideMenuItemCollectionControlViewModel
            {
                Items = new ObservableCollection<SideMenuItemControlViewModel>
                {
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Home,
                        DirectoryName = "Home", 
                        IsSelected = true,
                        ViewType = ViewType.HomeView
                    },
                }
            };
            // Quick access
            _quickAccessItems = new SideMenuItemCollectionControlViewModel
            {
                Items = new ObservableCollection<SideMenuItemControlViewModel>
                {
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "GitHub",
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Freelance",
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Embedded System Design",
                        ViewType = ViewType.DirectoryView
                    },
                }
            };
            // Main library
            _mainLibraryItems = new SideMenuItemCollectionControlViewModel
            {
                Items = new ObservableCollection<SideMenuItemControlViewModel>
                {
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Desktop",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Desktop),
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Documents",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Documents),
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Downloads,
                        DirectoryName = "Downloads",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Downloads),
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Pictures,
                        DirectoryName = "Pictures",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Pictures),
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Videos,
                        DirectoryName = "Videos",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Videos),
                        ViewType = ViewType.DirectoryView
                    },
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Music,
                        DirectoryName = "Music",
                        DirectoryFullPath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.Music),
                        ViewType = ViewType.DirectoryView
                    },
                }
            };
            // Drives
            _drivesItems = new SideMenuItemCollectionControlViewModel
            {
                Items = new ObservableCollection<SideMenuItemControlViewModel>
                {
                    new SideMenuItemControlViewModel(selectionEvent)
                    {
                        IconType = IconType.Drives,
                        DirectoryName = "Local Disk (C:)",
                        ViewType = ViewType.DrivesAndDevicesView
                    },
                }
            };

            #endregion
        }

        #endregion

    }
}
