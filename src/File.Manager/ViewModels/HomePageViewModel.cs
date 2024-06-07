using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Data;
using static File.Manager.DirectoryHelper;

namespace File.Manager
{
    /// <summary>
    /// View model for the <see cref="HomePage"/> of this application
    /// </summary>
    public class HomePageViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Tabs
        /// <remark>
        /// Used to organize logical drives and their associated data to their very own tab</remark>
        /// </summary>
        private ObservableCollection<TabItemModel> _tabs;

        /// <summary>
        /// The basic / common drive analysis
        /// </summary>
        private ObservableCollection<DriveStorageAnalysisViewModel> _driveFilesAnalysis;

        /// <summary>
        /// View model for <see cref="DriveBarControl"/>
        /// </summary>
        private DriveBarControlViewModel _driveBarControlVM;

        /// <summary>
        /// The list of recent directories
        /// </summary>
        private ObservableCollection<DirectoryItemControlViewModel> _recentDirectories;

        private DirectoryItemControlViewModel _directoryItemItemWithPopup;

        /// <summary>
        /// Selection changed event
        /// </summary>
        private SelectionChangedEvent _selectionChangedEvent;

        #endregion

        #region Public Properties

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

        /// <summary>
        /// The basic / common drive analysis
        /// </summary>
        public ObservableCollection<DriveStorageAnalysisViewModel> DriveFilesAnalysis
        {
            get => _driveFilesAnalysis;
            set
            {
                // If tabs value are not the same...
                if (_driveFilesAnalysis != value)
                    // Set tabs value 
                    _driveFilesAnalysis = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// View model for the drive bar
        /// </summary>
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
        /// The list of recent directories
        /// </summary>
        public ObservableCollection<DirectoryItemControlViewModel> RecentDirectories
        {
            get => _recentDirectories;
            set
            {
                // If value are not the same...
                if (_recentDirectories != value)
                    // Set value 
                    _recentDirectories = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Directory item pop-up data
        /// </summary>
        public DirectoryItemControlViewModel DirectoryItemItemWithPopup
        {
            get => _directoryItemItemWithPopup;
            set
            {
                // If value are not the same...
                if (_directoryItemItemWithPopup != value)
                    // Set value 
                    _directoryItemItemWithPopup = value;
                // Update properties
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The task to fetch information about a drive
        /// </summary>
        public Task DriveAnalysisTask { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePageViewModel()
        {
            // Set field defaults
            _tabs = new ObservableCollection<TabItemModel>();
            _driveBarControlVM = new DriveBarControlViewModel();
            _driveFilesAnalysis = new ObservableCollection<DriveStorageAnalysisViewModel>();
            _recentDirectories = new ObservableCollection<DirectoryItemControlViewModel>();
            _selectionChangedEvent = new SelectionChangedEvent();

            // Sets up logical drive
            SetupLogicalDriveUsedAndUnUsedSpaces();
            LoadRecentFolders();
            DriveAnalysisTask = LogicalDriveAnalysisAsync();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets up logical drive
        /// </summary>
        private void SetupLogicalDriveUsedAndUnUsedSpaces()
        {
            // Go through each drive on the machine
            Parallel.ForEach(DriveInfo.GetDrives(), drive =>
            {
                // Make sure drive is available
                if (drive.IsReady)
                {
                    // Convert drive values from byte to gigabyte
                    _driveBarControlVM.TotalDriveSize = ConvertByteToReadableValue(drive.TotalSize, 2);
                    _driveBarControlVM.MaxRange = ConvertByteToReadableValue(drive.TotalSize, 2, getJustTheValue: true);
                    _driveBarControlVM.CurrentMeterValue = ConvertByteToReadableValue(drive.TotalSize - drive.AvailableFreeSpace, 2, getJustTheValue: true);
                    _driveBarControlVM.UsedSpace = ConvertByteToReadableValue(drive.TotalSize - drive.AvailableFreeSpace, 2);
                    _driveBarControlVM.UnUsedSpace = ConvertByteToReadableValue(drive.AvailableFreeSpace, 2);

                    // Create label for each drive 
                    string label = GetLogicalDriveVolumeLabel(drive);
                    // Add each drive to tab item.
                    _tabs.Add(new TabItemModel { Header = label, Content = _driveBarControlVM });

                    // Reset drive bar item view model
                    _driveBarControlVM = new DriveBarControlViewModel();
                }

            });
        }

        /// <summary>
        /// Analyze logical drive
        /// </summary>
        private async Task LogicalDriveAnalysisAsync()
        {
            // Create pictures category
            var pictures = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Pictures,
                FileType = FileType.Pictures,
            };

            // Create videos category
            var videos = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Videos,
                FileType = FileType.Videos,
            };

            // Create music category
            var music = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Music,
                FileType = FileType.Music,
            };

            // Create documents category
            var documents = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Documents,
                FileType = FileType.Documents,
            };

            // Create temporary category
            var temporaryFiles = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Folder,
                FileType = FileType.TemporaryFiles,
            };

            // Create installed apps category
            var installedApps = new DriveStorageAnalysisViewModel()
            {
                IconType = IconType.Folder,
                FileType = FileType.InstalledApps,
            };

            // add categories to the list
            _driveFilesAnalysis.Add(music);
            _driveFilesAnalysis.Add(pictures);
            _driveFilesAnalysis.Add(videos);
            _driveFilesAnalysis.Add(documents);
            _driveFilesAnalysis.Add(temporaryFiles);
            _driveFilesAnalysis.Add(installedApps);

            // Get total sizes for each categories
            var musicFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            var pictureFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            var videoFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            var documentFilesTask = GetDirectorySizeAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var temporaryFilesTask = GetDirectorySizeAsync(Environment.GetEnvironmentVariable("TEMP") ?? throw new Exception("Environment variable not found"));
            var installedFilesTask = GetInstalledAppsTotalSizeAsync();

            // Wait for result to complete
            var result = await Task.WhenAll(pictureFilesTask, videoFilesTask, musicFilesTask, documentFilesTask, temporaryFilesTask, installedFilesTask);
            // Run on thread
            await Task.Run(() =>
            {
                // Set each total sizes to their respective categories
                music.TotalSizeOnDrive = musicFilesTask.Result;
                pictures.TotalSizeOnDrive = pictureFilesTask.Result;
                videos.TotalSizeOnDrive = videoFilesTask.Result;
                documents.TotalSizeOnDrive = documentFilesTask.Result;
                temporaryFiles.TotalSizeOnDrive = temporaryFilesTask.Result;
                installedApps.TotalSizeOnDrive = installedFilesTask.Result;
            });

            // Sort items in a descending order according to their sizes
            CollectionViewSource.GetDefaultView(DriveFilesAnalysis).SortDescriptions.Add(new SortDescription(nameof(DriveStorageAnalysisViewModel.RawTotalSizeOnDriveData), ListSortDirection.Descending));

            // Refresh drive files analysis collection to update it with the latest data
            //CollectionViewSource.GetDefaultView(DriveFilesAnalysis).Refresh();

        }

        /// <summary>
        /// Fetch and setup recently accessed directories
        /// </summary>
        public void LoadRecentFolders()
        {
            // Clear any existing directories
            _recentDirectories.Clear();

            // Get the path to recent folders
            string recentDirectories = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

            // Get the info about the path
            DirectoryInfo recentDirectoryInfo = new DirectoryInfo(recentDirectories);

            // Directory item selection event
            //_selectionChangedEvent selectionChangedEvent = new _selectionChangedEvent();

            try
            {

                // Go through files in the path
                foreach (FileInfo directory in recentDirectoryInfo.GetFiles())
                {
                    // Resolve path full name
                    string resolvedShortcut = ResolveShortcut(directory.FullName);
                    // If path exists...
                    if (Directory.Exists(resolvedShortcut))
                    {
                        // Create directory item control view model and set up information about this path with the view model
                        _recentDirectories.Add(new DirectoryItemControlViewModel(_selectionChangedEvent)
                        {
                            // Set properties
                            DirectoryName = directory.Name.Remove(directory.Name.LastIndexOf('.')),
                            LastDateAccessed = directory.LastAccessTime,
                            LastDateModified = directory.LastAccessTime.ToString("g"),
                            DirectoryItemType = "Recent Folder",
                            FullPath = resolvedShortcut,
                        });
                    }
                }
            } catch (Exception) { }

            // Sort recently accessed directories by the most recently accessed
            CollectionViewSource.GetDefaultView(RecentDirectories).SortDescriptions.Add(new SortDescription(nameof(DirectoryItemControlViewModel.LastDateAccessed), ListSortDirection.Descending));

            // Set number of directory items
            ServiceLocator.AppViewModel.NumberOfItemsInView = $"{_recentDirectories.Count} item(s)";

            // Update property
            OnPropertyChanged(nameof(RecentDirectories));
        }

        #endregion
    }
}