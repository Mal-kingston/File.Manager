using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for navigation bar <see cref="NavigationBarControl"/>
    /// </summary>
    public class NavigationBarViewModel : ViewModelBase
    {
        #region Private Fields

        /// <summary>
        /// The <see cref="INavigationService"/>
        /// </summary>
        private readonly INavigationService _navigationService = ServiceLocator.NavigationService;

        /// <summary>
        /// Current navigated directory path
        /// </summary>
        private ObservableCollection<NavigationBarPathItemViewModel> _currentDirectoryFullPath;

        #endregion

        #region Public Properties

        /// <summary>
        /// Current navigated directory path
        /// </summary>
        public ObservableCollection<NavigationBarPathItemViewModel> CurrentDirectoryFullPath
        {
            get => _currentDirectoryFullPath;
            set
            {
                // If value aren't same...
                if (_currentDirectoryFullPath != value)
                    // Set value
                    _currentDirectoryFullPath = value;
                // Update property
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// True if search bar is open, otherwise false
        /// </summary>
        public bool IsSearchBarOpen { get; set; } 

        /// <summary>
        /// Content of search button
        /// <remark>
        /// Shows a normal search icon when search bar is not visible
        /// But shows a close icon when search bar is visible
        /// </remark>
        /// </summary>
        public IconType SearchButtonIconType { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Command to navigate to previous page
        /// </summary>
        public ICommand NavigateToPreviousPageCommand { get; set; }

        /// <summary>
        /// Command to navigate to next page
        /// </summary>
        public ICommand NavigateToNextPageCommand { get; set; }

        /// <summary>
        /// Command to navigate to parent directory
        /// </summary>
        public ICommand NavigateToParentDirectoryCommand { get; set; }

        /// <summary>
        /// Command to open and close search bar
        /// </summary>
        public ICommand SearchBarCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavigationBarViewModel()
        {
            // Initialize objects
            _currentDirectoryFullPath = new ObservableCollection<NavigationBarPathItemViewModel>();
            SearchButtonIconType = IconType.Search;
            IsSearchBarOpen = false;

            // Create commands
            NavigateToPreviousPageCommand = new RelayCommand(NavigateToPreviousPage, canExecuteCommand => _navigationService.NavigatedPageCounter > 2);
            NavigateToNextPageCommand = new RelayCommand(NavigateToNextPage, canExecuteCommand => !(_navigationService.NavigatedPageCounter.Equals(_navigationService.NavigatedPageHistory.Count)));
            NavigateToParentDirectoryCommand = new RelayCommand(NavigateToParentDirectory, canExecuteCommand => _navigationService.CanNavigateToParentDirectory);
            SearchBarCommand = new RelayCommand(ToggleSearchBar, canExecuteCommand => true);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Navigates to the parent directory of a navigated path
        /// </summary>
        private void NavigateToParentDirectory() => ServiceLocator.NavigationService.NavigateToParentDirectory();

        /// <summary>
        /// Navigates to next page from the current page
        /// </summary>
        private void NavigateToNextPage() => ServiceLocator.NavigationService.NavigateToNextPage();

        /// <summary>
        /// Navigates to previous page from the current page
        /// </summary>
        private void NavigateToPreviousPage() => ServiceLocator.NavigationService.NavigateToPreviousPage();

        /// <summary>
        /// Toggles the search bar between visible and collapsed mode
        /// </summary>
        private void ToggleSearchBar()
        {
            // If value is true ? make is false (vice versa)
            IsSearchBarOpen ^= true;

            // Set search button icon type
            SearchButtonIconType = IsSearchBarOpen ? IconType.Close : IconType.Search;

            // Update properties
            OnPropertyChanged(nameof(SearchButtonIconType));
            OnPropertyChanged(nameof(IsSearchBarOpen));
        }



        #endregion

        #region Methods

        /// <summary>
        /// Sets navigated directory path using object passed in
        /// </summary>
        /// <param name="pathToDirectory">The object used to set navigated page path</param>
        public void SetNavigatedDirectoryPath(string pathToDirectory)
        {
            // Clear any directory path residue
            _currentDirectoryFullPath.Clear();

            // Sub-path variable
            string subPath;

            // Get path to user profile
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string userOneDriveProfilePath = DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.OneDrive);

            // If path to user includes One-Drive network drive...
            if(pathToDirectory.Contains(userOneDriveProfilePath))
                // Remove the path from incoming path
                subPath = (pathToDirectory).Replace(userOneDriveProfilePath, string.Empty);
            else
                // Remove the path from incoming path
                subPath = (pathToDirectory).Replace(userProfilePath, string.Empty);

            // Count backlashes on the path
            int backlashCount = subPath.Count(x => x.Equals('\\'));

            // If there are no backlashes
            if (backlashCount == 0)
            {
                // Set navigated path
                _currentDirectoryFullPath.Add(new NavigationBarPathItemViewModel() { DirectoryName = pathToDirectory });
            }

            // While we still have backlash...
            while (backlashCount > 0)
            {
                // If path is not null or empty...
                if (!string.IsNullOrEmpty(subPath))
                {
                    // Create a new path item
                    _currentDirectoryFullPath.Add(new NavigationBarPathItemViewModel()
                    {
                        // Extract directory name from sub-path
                        DirectoryName = subPath.Split('\\')[1]
                    });
                }

                // Remove the added path
                subPath = subPath.Remove(0, subPath.IndexOf('\\') + 1);

                // Decrease backlash count
                backlashCount--;
            }
        }

        #endregion
    }
}
