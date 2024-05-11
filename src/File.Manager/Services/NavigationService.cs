﻿using System.Windows.Navigation;

namespace File.Manager
{
    /// <summary>
    /// Service that facilitates navigation in this application
    /// </summary>
    public interface INavigationService
    {
        List<(ApplicationPages, string)> NavigatedPageHistory { get; set; }

        /// <summary>
        /// The page that is currently in view
        /// </summary>
        ApplicationPages CurrentPage { get; set; }

        /// <summary>
        /// The previous navigated page
        /// </summary>
        ApplicationPages PreviousPage { get; set; }

        /// <summary>
        /// The next navigated page (Occurs when user navigates to previous page)
        /// </summary>
        ApplicationPages NextPage { get; set; }

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        event EventHandler<object>? NewPageRequested;

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        void NavigateToPage(ApplicationPages page, string? pathToDirectory = null);

        void NavigateToPreviousPage();
        void NavigateToNextPage();

        ApplicationPages GetPage(ApplicationPages? page = null);

    }

    /// <summary>
    /// Service that facilitates navigation in this application
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// The page that is currently in view
        /// </summary>
        public ApplicationPages CurrentPage { get; set; }

        public List<(ApplicationPages, string)> NavigatedPageHistory { get; set; } = new List<(ApplicationPages, string)>();

        public ApplicationPages PreviousPage { get; set; }
        public ApplicationPages NextPage { get; set; }

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        public event EventHandler<object>? NewPageRequested;

        public ApplicationPages GetPage(ApplicationPages? page = null)
        {
            return ApplicationPages.None;
        }

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        /// <param name="pathToDirectory">The path to directory to navigate to if not null</param>
        public void NavigateToPage(ApplicationPages page, string? pathToDirectory)
        {
            // If we have path to a directory...
            if(pathToDirectory != null)
            {
                // Load the directory of the path
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(pathToDirectory);

                // Set nav-bar path to directory
                ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath(pathToDirectory);
            }

            // If current page is not up to date
            if (CurrentPage != page)
            {
                // Update page
                CurrentPage = page;
                // Raise new_page_requested_event
                OnNewPageRequested();
            }

            UpdateNavigatedPageHistory(page, pathToDirectory!);
        }

        public void NavigateToNextPage()
        {
            CurrentPage = NextPage;

            //UpdateNavigatedPageHistory();

            OnNewPageRequested();
        }

        public void NavigateToPreviousPage()
        {
            CurrentPage = PreviousPage;

            //UpdateNavigatedPageHistory();

            OnNewPageRequested();
        }

        private void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath)
        {
            NavigatedPageHistory.Add((page, currentPagePath));
        }

        /// <summary>
        /// Raises the <see cref="NewPageRequested"/> event
        /// </summary>
        protected virtual void OnNewPageRequested()
        {
            NewPageRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
