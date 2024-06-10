using System.Collections.ObjectModel;
using System.IO;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryExplorer"/>
    /// </summary>
    public class DirectoryExplorerViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// The list of current directory items [ folders | files ]
        /// </summary>
        private ObservableCollection<DirectoryItemControlViewModel> _directories;

        /// <summary>
        /// The directory item whose pop-up menu is open
        /// </summary>
        private DirectoryItemControlViewModel _directoryItemItemWithPopup;

        /// <summary>
        /// Event to fire when <see cref="DirectoryItemControlViewModel"/> item is selected
        /// </summary>
        private SelectionChangedEvent _selectionChangedEvent;

        #endregion

        #region Public Properties

        /// <summary>
        /// The list of current directory items [ folders | files ]
        /// </summary>
        public ObservableCollection<DirectoryItemControlViewModel> Directories 
        {
            get => _directories;

            set
            {
                // If directories value isn't up to date
                if (_directories != value) 
                    // Update directories
                    _directories = value;

                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Directory item Popup data
        /// </summary>
        public DirectoryItemControlViewModel DirectoryItemItemWithPopup
        {
            get => _directoryItemItemWithPopup;
            set
            {
                if(_directoryItemItemWithPopup != value)
                    _directoryItemItemWithPopup = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// True if directory is empty, otherwise false
        /// </summary>
        public bool IsDirectoryEmpty => _directories.Count == 0;

        /// <summary>
        /// Type of a directory item [ folders | files ]
        /// </summary>
        public FileType DirectoryItemType { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryExplorerViewModel()
        {
            // Set defaults
            _selectionChangedEvent = new SelectionChangedEvent();
            _directories = new ObservableCollection<DirectoryItemControlViewModel>();
            _directoryItemItemWithPopup = new DirectoryItemControlViewModel(_selectionChangedEvent);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load items of a specified directory
        /// </summary>
        /// <param name="fullPath">The full fullPath to the directory to load</param>
        public void LoadDirectoryItems(string fullPath)
        {
            // Remove any existing directory
            _directories.Clear();

            // Get information about the directory items to load
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

            try
            {
                // Go through every directory
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    // Make sure directory item is usable by the operating system
                    if(!directory.Attributes.HasFlag(FileAttributes.NotContentIndexed))
                    {
                        // Add directory to list of directories
                        _directories.Add(new DirectoryItemControlViewModel(_selectionChangedEvent)
                        {
                            IconType = IconType.Folder,
                            DirectoryName = directory.Name,
                            LastDateModified = directory.LastWriteTime.ToString("g"),
                            DirectoryItemType = "Folder",
                            FullPath = directory.FullName,
                        });
                    }
                }

                // Go through every directory
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    // Filter non user files
                    if(!file.Attributes.HasFlag(FileAttributes.System))
                    {
                        // Add file to the list of directories
                        _directories.Add(new DirectoryItemControlViewModel(_selectionChangedEvent)
                        {
                            DirectoryName = file.Name,
                            LastDateModified = file.LastWriteTime.ToString("g"),
                            DirectoryItemType = DirectoryHelper.GetFileType(file.FullName),
                            IconType = DirectoryHelper.GetFileIconType(DirectoryHelper.GetFileType(file.FullName)),
                            FullPath = file.FullName,
                            SizeOfDirectoryItem = DirectoryHelper.ConvertByteToReadableValue(file.Length, 2)
                        });
                    }
                }
            }
            catch (Exception) { }

            // Set nav-bar directory path 
            ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(fullPath);

            // Set number of directory items
            ServiceLocator.AppViewModel.NumberOfItemsInView = $"{_directories.Count} item(s)";

            // Update properties
            OnPropertyChanged(nameof(IsDirectoryEmpty));
        }

        #endregion
    }
}
