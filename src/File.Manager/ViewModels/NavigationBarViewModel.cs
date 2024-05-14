﻿using System.Collections.ObjectModel;
using System.Windows.Input;

namespace File.Manager
{
    /// <summary>
    /// View model for navigation bar <see cref="NavigationBarControl"/>
    /// </summary>
    public class NavigationBarViewModel : ViewModelBase
    {
        /// <summary>
        /// Current navigated directory path
        /// </summary>
        private ObservableCollection<NavigationBarPathItemViewModel> _currentDirectoryFullPath;

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
        /// Command to navigate to previous page
        /// </summary>
        public ICommand NavigateToPreviousPageCommand { get; set; }

        /// <summary>
        /// Command to navigate to next page
        /// </summary>
        public ICommand NavigateToNextPageCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavigationBarViewModel()
        {
            // Initialize objects
            _currentDirectoryFullPath = new ObservableCollection<NavigationBarPathItemViewModel>();

            // Create commands
            NavigateToPreviousPageCommand = new RelayCommand(NavigateToPreviousPage, canExecuteCommand => true);
            NavigateToNextPageCommand = new RelayCommand(NavigateToNextPage, canExecuteCommand => true);
        }

        /// <summary>
        /// Navigates to next page from the current page
        /// </summary>
        private void NavigateToNextPage() => ServiceLocator.NavigationService.NavigateToNextPage();

        /// <summary>
        /// Navigates to previous page from the current page
        /// </summary>
        private void NavigateToPreviousPage() => ServiceLocator.NavigationService.NavigateToPreviousPage();

        /// <summary>
        /// Sets navigated directory path using object passed in
        /// </summary>
        /// <param name="pathToDirectory">The object used to set navigated page path</param>
        public void SetNavigatedDirectoryPath(string pathToDirectory)
        {
            // Clear any directory path residue
            _currentDirectoryFullPath.Clear();

            // Get path to user profile
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Remove path to user profile from incoming path
            string subPath = ((string)pathToDirectory).Replace(userProfilePath, string.Empty);

            // Count backlashes on the path
            int backlashCount = subPath.Count(x => x.Equals('\\'));

            // If there are no backlashes
            if (backlashCount == 0)
            {
                // Set navigated path
                _currentDirectoryFullPath.Add(new NavigationBarPathItemViewModel() { DirectoryName = (string)pathToDirectory });
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
    }
}
