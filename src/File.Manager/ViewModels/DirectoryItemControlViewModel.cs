using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryItemControl"/>
    /// </summary>
    public class DirectoryItemControlViewModel : ViewModelBase
    {
        private readonly SelectionChangedEvent _selectionChanged;

        #region Public Properties

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
            // If current page isn't directoryItem explorer...
            if (ServiceLocator.AppViewModel.CurrentPage != ApplicationPages.DirectoryExplorer)
                // Navigate to directoryItem explorer page with the specified path
                ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.DirectoryExplorer, FullPath);
            // Otherwise if we are already in directoryItem explorer page...
            else
                // Load path directoryItem into view
                ServiceLocator.DirectoryExplorerViewModel.LoadDirectoryItems(FullPath);
        }

        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            // Cast sender as directoryItem control view-model
            var directoryItem = (sender as DirectoryItemControlViewModel);

            // Reset selection
            IsChecked = false;
            // If current item is the currently item that user is clicked on...
            if (DirectoryName.Equals(directoryItem?.DirectoryName))
                // Mark is selected
                IsChecked = true;

            // Update property
            OnPropertyChanged(nameof(IsChecked));
        }

        #endregion

    }
}
