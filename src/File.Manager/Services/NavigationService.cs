﻿using System.IO;
using System.Linq;

namespace File.Manager
{
    /// <summary>
    /// Service that facilitates navigation in this application
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigation history
        /// </summary>
        List<(ApplicationPages, string)> NavigatedPageHistory { get; set; }

        /// <summary>
        /// The page that is currently in view
        /// </summary>
        ApplicationPages CurrentPage { get; set; }

        /// <summary>
        /// The previous navigated page
        /// </summary>
        (ApplicationPages, string) PreviousPage { get; set; }

        /// <summary>
        /// The next navigated page (Occurs when user navigates to previous page)
        /// </summary>
        (ApplicationPages, string)  NextPage { get; set; }

        /// <summary>
        /// Keeps track of navigated pages when going through navigated page history
        /// </summary>
        int NavigatedPageCounter => 0;

        /// <summary>
        /// True if we can navigate to parent directory, otherwise false
        /// </summary>
        bool CanNavigateToParentDirectory { get; }

        /// <summary>
        /// Mode of navigation
        /// </summary>
        NavigationMode NavigationMode { get; set; }

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        event EventHandler<object>? NewPageRequested;

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        void NavigateToPage(ApplicationPages page, string pathToDirectory);

        /// <summary>
        /// Captures and keeps the navigated page history
        /// </summary>
        /// <param name="page">The page type <see cref="ApplicationPages"/></param>
        /// <param name="currentPagePath">Path to navigated page item</param>
        void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath);

        /// <summary>
        /// Navigates to the previously navigated page
        /// </summary>
        void NavigateToPreviousPage();

        /// <summary>
        /// Navigates to the next navigated page
        /// </summary>
        void NavigateToNextPage();

        /// <summary>
        /// Navigates to the parent directory of the currently navigated directory
        /// </summary>
        void NavigateToParentDirectory();
    }

    /// <summary>
    /// Service that facilitates navigation of this application
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Public Properties

        /// <summary>
        /// The page that is currently in view
        /// </summary>
        public ApplicationPages CurrentPage { get; set; }

        /// <summary>
        /// Navigation history
        /// </summary>
        public List<(ApplicationPages, string)> NavigatedPageHistory { get; set; } = new List<(ApplicationPages, string)>() { (ApplicationPages.None, "") };

        /// <summary>
        /// Keeps track of navigated pages when going through navigated page history
        /// </summary>
        public int NavigatedPageCounter { get; private set; } = 0;

        /// <summary>
        /// Mode of navigation
        /// </summary>
        public NavigationMode NavigationMode { get; set; } = NavigationMode.NewPage;

        /// <summary>
        /// The previous page to navigate back to
        /// </summary>
        public (ApplicationPages, string) PreviousPage { get; set; }

        /// <summary>
        /// The next page page to navigate forward to
        /// </summary>
        public (ApplicationPages, string) NextPage { get; set; }

        /// <summary>
        /// True if we can navigate to parent directory, otherwise false
        /// </summary>
        public bool CanNavigateToParentDirectory => CanNavigateToParent();

        #endregion

        #region Public Events

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        public event EventHandler<object>? NewPageRequested;

        #endregion

        #region Methods

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        /// <param name="pathToDirectory">The path to directory to navigate to if not null</param>
        public void NavigateToPage(ApplicationPages page, string pathToDirectory)
        {
            // If we have no page...
            if (page.Equals(ApplicationPages.None))
                // Do nothing
                return;

            // Navigation mode is to previous or next page...
            if(NavigationMode.Equals(NavigationMode.PreviousPage) || NavigationMode.Equals(NavigationMode.NextPage))
            {
                // Reset navigated page history and counter
                //NavigatedPageHistory.RemoveRange(2, NavigatedPageHistory.Count - 2);

                // Reset counter
                //NavigatedPageCounter = 0;

                // Set navigation mode
                NavigationMode = NavigationMode.NewPage;
            }

            // Keep navigated page history
            UpdateNavigatedPageHistory(page, pathToDirectory);

            // If we have path to a directory...
            //if (pathToDirectory.Contains(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) || pathToDirectory.Contains("C:\\"))
            if (Directory.Exists(pathToDirectory))
                // Load the directory of the path
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(pathToDirectory);
            else
                // Set navigated page path
                ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(pathToDirectory);

            // Change page
            CurrentPage = page;

            // Raise new_page_requested_event
            OnNewPageRequested();
        }

        /// <summary>
        /// Navigates to the next navigated page
        /// </summary>
        public void NavigateToNextPage()
        {
            // If navigation mode isn't next-page...
            if (!NavigationMode.Equals(NavigationMode.NextPage))
                // Set navigation mode
                NavigationMode = NavigationMode.NextPage;

            // If counter is not equals to the number of pages in navigated-page-history...
            if (!NavigatedPageCounter.Equals(NavigatedPageHistory.Count))
                // Increase counter
                NavigatedPageCounter++;
            // Otherwise
            else
                // Do nothing
                return;

            // Get the next page
            NextPage = NavigatedPageHistory[NavigatedPageCounter - 1];

            // If we don't have a valid page...
            if (PreviousPage.Item1.Equals(ApplicationPages.None))
                // Do nothing
                return;

            // Set current page
            CurrentPage = NextPage.Item1;

            // If page path contains path to user profile...
            //if (NextPage.Item2.Contains(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
            if (Directory.Exists(NextPage.Item2))
                // Load the path
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(NextPage.Item2);
            // If not...
            else
                // Set navigation path
                ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(NextPage.Item2);

            // Raise new_page_requested_event
            OnNewPageRequested();
        }

        /// <summary>
        /// Navigates to the previously navigated page
        /// </summary>
        public void NavigateToPreviousPage()
        {
            // If navigation mode isn't previous-page...
            if(!NavigationMode.Equals(NavigationMode.PreviousPage))
                // Set navigation mode
                NavigationMode = NavigationMode.PreviousPage;

            //// If counter is zero [ 0 ]
            //if(NavigatedPageCounter == 0)
            //    // Get the number of navigated pages
            //    NavigatedPageCounter = NavigatedPageHistory.Count;

            // Get the previous page
            PreviousPage = NavigatedPageHistory[NavigatedPageCounter - 2];

            // If we don't have a valid page...
            if (PreviousPage.Item1.Equals(ApplicationPages.None))
                // Do nothing
                return;
            // Otherwise
            else
                // Set current page
                CurrentPage = PreviousPage.Item1;

            // If page path contains path to user profile...
            //if (PreviousPage.Item2.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
            //if (PreviousPage.Item2.Contains("\\"))

            // If page path exist...
            if (Directory.Exists(PreviousPage.Item2))
                // Load the path
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(PreviousPage.Item2);
            // If not...
            else
                // Set navigation path
                ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(PreviousPage.Item2);

            // If counter is not equals to 2
            if (!NavigatedPageCounter.Equals(2))
                // Decrease counter
                NavigatedPageCounter--;
            // Otherwise...
            else
                // Reset counter
                NavigatedPageCounter = 0;

            // Raise new_page_requested_event
            OnNewPageRequested();
        }

        /// <summary>
        /// Navigates to the parent directory of the currently navigated directory
        /// </summary>
        public void NavigateToParentDirectory()
        {
            // Current directory variable
            string currentDirectory;
            // Flag
            bool isPathFromNavigationHistory = false;

            // If we have a directory item 
            if (!ServiceLocator.DirectoryExplorerVM.Directories.Count.Equals(0))
                // Get the first item of the current directory
                currentDirectory = ServiceLocator.DirectoryExplorerVM.Directories[0].FullPath;
            // Otherwise
            else
            {
                // Get the path from history
                currentDirectory = NavigatedPageHistory[NavigatedPageCounter - 1].Item2;
                // Set flag
                isPathFromNavigationHistory = true;
            }

            // Get information about the directory
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(currentDirectory);

            // If parent directory of current directory isn't null...
            if (currentDirectoryInfo.Parent != null)
            {
                // Get the full path
                string parentDirectoryPath = currentDirectoryInfo.Parent.FullName;

                // If we are using path from nav-history...
                if (isPathFromNavigationHistory)
                {
                    // Reset flag
                    isPathFromNavigationHistory = false;

                    // Keep path the same
                    parentDirectoryPath = currentDirectoryInfo.Parent.FullName;
                }
                // If we have a directory item 
                else if (parentDirectoryPath.Count(x => x.Equals('\\')) > 1)
                    // The actual parent directory path
                    parentDirectoryPath = parentDirectoryPath.Remove(parentDirectoryPath.LastIndexOf("\\"));
                else
                    parentDirectoryPath = parentDirectoryPath.Remove(parentDirectoryPath.LastIndexOf("\\") + 1);

                // Get side menu view model
                SideMenuControlViewModel sideMenuViewModel = ServiceLocator.SideMenuControlVM;

                // If path is not the same as user profile...
                if (!parentDirectoryPath.Equals(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
                    // Navigate to the parent directory
                    NavigateToPage(ApplicationPages.DirectoryExplorer, parentDirectoryPath);
                // If we're in storage... 
                else if (sideMenuViewModel.DrivesItems.Items[0].IsSelected && !sideMenuViewModel.MainLibraryItems.Items.Any(x => x.IsSelected.Equals(true)))
                    // Ignore user-profile and navigate to parent directory
                    NavigateToPage(ApplicationPages.DirectoryExplorer, parentDirectoryPath);
            }
        }

        /// <summary>
        /// Captures and keeps the navigated page history
        /// </summary>
        /// <param name="page">The page type <see cref="ApplicationPages"/></param>
        /// <param name="currentPagePath">Path to navigated page item</param>
        public void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath)
        {
            // If page in navigation is not the same as the previously navigated page...
            if (!(NavigatedPageHistory[NavigatedPageHistory.Count - 1].Equals((page, currentPagePath))))
            {
                // Make sure we are navigating to a new page...
                if (NavigatedPageCounter > 0 && NavigationMode.Equals(NavigationMode.NewPage))
                    // Remove page(s) that are not needed anymore
                    NavigatedPageHistory.RemoveRange(NavigatedPageCounter, NavigatedPageHistory.Count - NavigatedPageCounter);

                // Add it to the navigation history
                NavigatedPageHistory.Add((page, currentPagePath));
            }

            // Keep track of number of navigated page
            NavigatedPageCounter = NavigatedPageHistory.Count;

            // Raise new_page_requested_event
            OnNewPageRequested();
        }

        /// <summary>
        /// Determines if we can navigate to the parent directory of the current directory in view 
        /// </summary>
        /// <returns>True if we can navigate to the parent directory, otherwise false</returns>
        public bool CanNavigateToParent()
        {
            // Get the current directory
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(NavigatedPageHistory[NavigatedPageCounter - 1].Item2);
            // Get the breaking point path
            string breakPoint = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Make sure parent of the current directory is not null
            if (currentDirectoryInfo.Parent == null || !(NavigatedPageHistory[NavigatedPageCounter - 1].Item2.Contains("\\")))
                // If not, return false
                return false;

            // Get side menu view model
            SideMenuControlViewModel sideMenuViewModel = ServiceLocator.SideMenuControlVM;

            // If we're in storage... Ignore breakpoint
            if (sideMenuViewModel.DrivesItems.Items[0].IsSelected && !sideMenuViewModel.MainLibraryItems.Items.Any(x => x.IsSelected.Equals(true)) && currentDirectoryInfo.Parent.FullName.Equals(breakPoint))
                return true;

            // Return false is current directory parent's full name is same as break-point, otherwise return true
            return currentDirectoryInfo.Parent.FullName.Equals(breakPoint) || currentDirectoryInfo.Parent.FullName.Equals(DirectoryHelper.GetDefaultDirectoryPath(DefaultDirectoryType.OneDrive)) ? false : true;
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Raises the <see cref="NewPageRequested"/> event
        /// <remark>Also triggers a condition that checks if navigating to parent directory is allowed</remark>
        /// </summary>
        protected virtual void OnNewPageRequested()
        {
            NewPageRequested?.Invoke(this, EventArgs.Empty);

            // Determines if we can navigate to parent directory
            CanNavigateToParent();
        }

        #endregion
    }
}