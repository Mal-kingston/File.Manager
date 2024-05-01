using System.Collections.ObjectModel;
using System.IO;
using static File.Manager.DirectoryHelper;


namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryExplorer"/>
    /// </summary>
    public class DirectoryExplorerViewModel : ViewModelBase
    {
        /// <summary>
        /// The list of current directory items [ folders | files ]
        /// </summary>
        private ObservableCollection<DirectoryItemControlViewModel> _directories;

        /// <summary>
        /// The list of current directory items [ folders | files ]
        /// </summary>
        public ObservableCollection<DirectoryItemControlViewModel> Directories 
        {
            get => _directories;

            set
            {
                if (_directories != value) 
                    _directories = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// True if directory is empty, otherwise false
        /// </summary>
        public bool IsDirectoryEmpty => _directories.Count == 0;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryExplorerViewModel()
        {
            // Set defaults
            _directories = new ObservableCollection<DirectoryItemControlViewModel>();
        }

        /// <summary>
        /// Load items of a specified directory
        /// </summary>
        /// <param name="fullPath">The full fullPath to the directory to load</param>
        public void LoadDirectoryItems(string fullPath)
        {
            // Remove any existing directory
            _directories.Clear();

            // Get information about the directory to load
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

            // Go through every directory
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                // Add directory to list of directories
                _directories.Add(new DirectoryItemControlViewModel
                {
                    DirectoryName = directory.Name,
                    LastDateModified = directory.LastWriteTime.ToString("g"),
                    DirectoryItemType = "File folder",
                    //SizeOfDirectoryItem = ConvertByteToReadableValue(directory., 2),
                    FullPath = ResolveShortcut(directory.FullName),
                });
            }

            // Update property
            OnPropertyChanged(nameof(IsDirectoryEmpty));
        }
    }
}
