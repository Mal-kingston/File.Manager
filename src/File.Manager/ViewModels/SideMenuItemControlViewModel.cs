using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for <see cref="DirectoryControl"/> of this application
    /// </summary>
    public class SideMenuItemControlViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// Custom event for whenever this item is selected
        /// </summary>
        private readonly SelectionChangedEvent _selectionEvent;

        /// <summary>
        /// Default directory name
        /// </summary>
        private string _directoryName = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of this directory
        /// </summary>
        public string DirectoryName 
        {
            // Get field value
            get => _directoryName;
            set
            {
                // if value doesn't match field...
                if (_directoryName != value)
                    // Set field to match current value
                    _directoryName = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Full path of this directory
        /// </summary>
        public string DirectoryFullPath { get; set; } = string.Empty;

        /// <summary>
        /// The Current icon type of this directory
        /// </summary>
        public IconType IconType { get; set; }

        /// <summary>
        /// Types of views
        /// </summary>
        public ViewType ViewType { get; set; }

        /// <summary>
        /// True if this item is current selected in the view
        /// otherwise false.
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Modifies this directory item properties
        /// [ Rename | Change directory icon | Pin or unpin from quick-access collection ]
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Command to initiate the selection of this item
        /// </summary>
        public ICommand IsSelectedCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemControlViewModel(SelectionChangedEvent selectionEvent)
        {
            // Set default data to use
            DirectoryName = "Documents";
            IconType = IconType.Folder;

            // Events
            _selectionEvent = selectionEvent;

            // Hook up to the selection-changed event
            _selectionEvent.SelectionChanged += OnSelectionChanged;

            // Create commands
            EditCommand = new RelayCommand(EditDirectory, canExecuteCommand => this != null);
            IsSelectedCommand = new RelayCommand((parameter) => 
            {
                GotoSelectedPage((SideMenuItemControlViewModel)parameter);
                _selectionEvent.ItemSelected(this);
            }, canExecuteCommand => this != null);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Modifies this directory item properties
        /// [ Rename | Change directory icon | Pin or unpin from quick-access collection ]
        /// </summary>
        private void EditDirectory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Goes to page that the selected object points to
        /// </summary>
        /// <param name="SideMenuItemControlViewModel">The selected item object</param>
        public void GotoSelectedPage(SideMenuItemControlViewModel sideMenuItemControlViewModel)
        {
            // Sort and navigate to appropriate views
            switch (sideMenuItemControlViewModel.ViewType)
            {
                // Home view
                case ViewType.HomeView:
                    ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(DirectoryName);
                    ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.Home);
                    break;

                // Directory view
                case ViewType.DirectoryView:
                    ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.DirectoryExplorer, DirectoryFullPath);
                    ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(DirectoryName);
                    break;

                // Drives and devices view
                case ViewType.DrivesAndDevicesView:
                    ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(DirectoryName);
                    ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.DrivesAndDevices);
                    break;

                // Default 
                default:
                    throw new Exception("Application page not found");
            }

        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Resets selection whenever selection changes
        /// </summary>
        /// <param name="directory">The currently selected item</param>
        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            // Cast sender as directory control view-model
            var directory = (sender as SideMenuItemControlViewModel);

            // Reset selection
            IsSelected = false;
            // If current item is the currently item that user clicked on...
            if (DirectoryName.Equals(directory?.DirectoryName))
                // Mark is selected
                IsSelected = true;

            // Update property
            OnPropertyChanged(nameof(IsSelected));
        }

        #endregion
    }
}
