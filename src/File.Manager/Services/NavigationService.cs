using System.Runtime.CompilerServices;
using System.Windows.Navigation;

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
        (ApplicationPages, string) PreviousPage { get; set; }

        /// <summary>
        /// The next navigated page (Occurs when user navigates to previous page)
        /// </summary>
        (ApplicationPages, string)  NextPage { get; set; }

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        event EventHandler<object>? NewPageRequested;

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        void NavigateToPage(ApplicationPages page, string pathToDirectory);

        void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath);

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

        /// <summary>
        /// Navigation history
        /// </summary>
        public List<(ApplicationPages, string)> NavigatedPageHistory { get; set; } = new List<(ApplicationPages, string)>() { (ApplicationPages.None, "") };

        private int NavigatedPageCounter = 0;

        private NavigationMode NavigationMode = NavigationMode.NewPage;

        /// <summary>
        /// The previous page to navigate back to
        /// </summary>
        public (ApplicationPages, string) PreviousPage { get; set; }

        /// <summary>
        /// The next page page to navigate forward to
        /// </summary>
        public (ApplicationPages, string) NextPage { get; set; }

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
        public void NavigateToPage(ApplicationPages page, string pathToDirectory)
        {
            // If we have no page...
            if (page.Equals(ApplicationPages.None))
                // Do nothing
                return;

            // 
            if(NavigationMode.Equals(NavigationMode.PreviousPage))
            {
                // Reset navigated page history and counter
                NavigatedPageHistory.RemoveRange(2, NavigatedPageHistory.Count - 2);

                // Reset counter
                NavigatedPageCounter = 0;

                // Set navigation mode
                NavigationMode = NavigationMode.NewPage;
            }

            // Keep navigated page history
            UpdateNavigatedPageHistory(page, pathToDirectory);

            // If we have path to a directory...
            if (pathToDirectory.Contains(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
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

        public void NavigateToNextPage()
        {
            //CurrentPage = NextPage;

            //UpdateNavigatedPageHistory();

            //OnNewPageRequested();
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

            // If counter is zero [ 0 ]
            if(NavigatedPageCounter == 0)
                // Get the number of navigated pages
                NavigatedPageCounter = NavigatedPageHistory.Count;

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
            if (PreviousPage.Item2.Contains(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
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
        /// Captures and keeps the navigated page history
        /// </summary>
        /// <param name="page">The page type <see cref="ApplicationPages"/></param>
        /// <param name="currentPagePath">Path to navigated page item</param>
        public void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath)
        {
            // If page in navigation is not the same as the previously navigated page...
            if (!(NavigatedPageHistory[NavigatedPageHistory.Count - 1].Equals((page, currentPagePath))))
                // Add it to the navigation history
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
