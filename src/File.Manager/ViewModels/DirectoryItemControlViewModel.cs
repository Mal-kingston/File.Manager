using IWshRuntimeLibrary;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryItemControl"/>
    /// </summary>
    public class DirectoryItemControlViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Event that gets fired when this item is selected
        /// </summary>
        private readonly SelectionChangedEvent _selectionChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// The type of icon used for this item
        /// </summary>
        public IconType IconType { get; set; }

        /// <summary>
        /// The name of directoryItem
        /// </summary>
        public string DirectoryName { get; set; } = string.Empty;

        /// <summary>
        /// Date this directoryItem was last accessed
        /// </summary>
        public DateTime LastDateAccessed { get; set; }

        /// <summary>
        /// Date this directoryItem was last written to or made changes to
        /// </summary>
        public string LastDateModified { get; set; } = string.Empty;

        /// <summary>
        /// The type of this directoryItem
        /// </summary>
        public string DirectoryItemType { get; set; } = string.Empty;

        /// <summary>
        /// The size of this directoryItem
        /// </summary>
        public string SizeOfDirectoryItem { get; set; } = string.Empty;

        /// <summary>
        /// The full path of this directoryItem
        /// </summary>
        public string FullPath { get; set; } = string.Empty;

        /// <summary>
        /// True if this item is checked, otherwise false
        /// </summary>
        public bool IsChecked { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command to run when a directoryItem item is selected
        /// </summary>
        public ICommand SelectItemCommand { get; set; }

        /// <summary>
        /// Command to check directoryItem item on single click
        /// </summary>
        public ICommand IsCheckedCommand { get; set; }

        #endregion

        #region Constructor

        public DirectoryItemControlViewModel(SelectionChangedEvent selectionChanged)
        {
            // Set defaults 
            _selectionChanged = selectionChanged;

            // event hook up
            _selectionChanged.SelectionChanged += OnSelectionChanged;

            // Create commands
            SelectItemCommand = new RelayCommand(OpenDirectoryItem, canExecuteCommand => this != null);
            IsCheckedCommand = new RelayCommand(() => _selectionChanged.ItemSelected(this), canExecuteCommand => this != null);

        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Opens the selected directoryItem item on double click
        /// </summary>
        private void OpenDirectoryItem()
        {
            // TODO: Handle opening and extracting a zipped folder

            // If full path is not of type directory...
            if (!Directory.Exists(FullPath))
            {
                // Open the file
                LoadFile(FullPath);

                // Reload recent directory with the most recent data
                ServiceLocator.HomePageVM.LoadRecentFolders();

                // Do nothing else
                return;
            }

            // If current page isn't directoryItem explorer...
            if (ServiceLocator.AppViewModel.CurrentPage != ApplicationPages.DirectoryExplorer)
                // Navigate to directoryItem explorer page with the specified path
                ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.DirectoryExplorer, FullPath);
            // Otherwise if we are already in directoryItem explorer page...
            else
            {
                // Load path directoryItem into view
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(FullPath);
                // Set navigation mode
                ServiceLocator.NavigationService.NavigationMode = NavigationMode.NewPage;
                // Keep record of navigated view
                ServiceLocator.NavigationService.UpdateNavigatedPageHistory(ServiceLocator.NavigationService.CurrentPage, FullPath);
            }
        }

        /// <summary>
        /// Loads / opens a file
        /// </summary>
        /// <param name="fullPath">The full path of the file to open</param>
        private void LoadFile(string fullPath)
        {
            // TODO: Try opening a zipped folder correctly
            try
            {
                // if path points to a zip folder
                if (fullPath.EndsWith(".zip"))
                {
                    // Remove extension to be able to load the zip folder
                    fullPath = fullPath.Replace(".zip", string.Empty);

                    // Load path to view
                    ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(fullPath);

                    // Do nothing else
                    return;
                }

                // Create processInfo
                ProcessStartInfo processInfo = new ProcessStartInfo(fullPath)
                {
                    Arguments = Path.GetFileName(fullPath),
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(fullPath),
                    FileName = fullPath,
                    Verb = "OPEN"
                };
                // Start processInfo
                Process.Start(processInfo);

            } catch (Exception) { }
        }

        /// <summary>
        /// Called when this item is checked to reset this item as the currently checked item
        /// </summary>
        /// <param name="sender">The source of event</param>
        /// <param name="e">Event args</param>
        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            // Cast sender as directoryItem control view-model
            DirectoryItemControlViewModel? directoryItem = (sender as DirectoryItemControlViewModel);

            // Reset selection
            IsChecked = false;
            // If current item is the currently item that user clicked on...
            if (DirectoryName.Equals(directoryItem?.DirectoryName))
                // Mark is selected
                IsChecked = true;

            // Update property
            OnPropertyChanged(nameof(IsChecked));

        }

        #endregion
    }
}
