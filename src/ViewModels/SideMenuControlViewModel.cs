using System.Collections.ObjectModel;

namespace File.Manager
{
    public class SideMenuControlViewModel : ViewModelBase
    {
        private DirectoryCollectionControlViewModel _overViewItems;
        private DirectoryCollectionControlViewModel _quickAccessItems;
        private DirectoryCollectionControlViewModel _mainLibraryItems;
        private DirectoryCollectionControlViewModel _drivesItems;

        public DirectoryCollectionControlViewModel OverViewItems 
        {
            get => _overViewItems;
            set
            {
                if (_overViewItems != value)
                    _overViewItems = value;
                OnPropertyChanged();
            }
        }

        public DirectoryCollectionControlViewModel QuickAccessItems
        {
            get => _quickAccessItems;
            set
            {
                if (_quickAccessItems != value)
                    _quickAccessItems = value;
                OnPropertyChanged();
            }
        }

        public DirectoryCollectionControlViewModel MainLibraryItems
        {
            get => _mainLibraryItems;
            set
            {
                if (_mainLibraryItems != value)
                    _mainLibraryItems = value;
                OnPropertyChanged();
            }
        }

        public DirectoryCollectionControlViewModel DrivesItems
        {
            get => _drivesItems;
            set
            {
                if (_drivesItems != value)
                    _drivesItems = value;
                OnPropertyChanged();
            }
        }

        public SideMenuControlViewModel()
        {
            _overViewItems =  new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.ThisPC,
                        DirectoryName = "This PC",
                    },
                }
            };

            _quickAccessItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Github",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Freelance",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Embedded System Design",
                    },
                }
            };

            _mainLibraryItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Desktop",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Documents",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Downloads,
                        DirectoryName = "Downloads",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Pictures,
                        DirectoryName = "Pictures",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Videos,
                        DirectoryName = "Videos",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Music,
                        DirectoryName = "Music",
                    },




                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Desktop",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Folder,
                        DirectoryName = "Documents",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Downloads,
                        DirectoryName = "Downloads",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Pictures,
                        DirectoryName = "Pictures",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Videos,
                        DirectoryName = "Videos",
                    },
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Music,
                        DirectoryName = "Music",
                    },
                }
            };

            _drivesItems = new DirectoryCollectionControlViewModel
            {
                DirectoryItems = new ObservableCollection<DirectoryControlViewModel>
                {
                    new DirectoryControlViewModel
                    {
                        IconType = IconType.Drives,
                        DirectoryName = "Local Disk (C:)",
                    },
                }
            };

        }
    }
}
