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
        void NavigateToPage(ApplicationPages page, string? pathToDirectory = null);

        void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath);

        void NavigateToPreviousPage(string? pagePath = null);
        void NavigateToNextPage(string? pagePath = null);

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
        public void NavigateToPage(ApplicationPages page, string? pathToDirectory)
        {
            // Keep navigated page history
            UpdateNavigatedPageHistory(page, pathToDirectory ?? string.Empty);

            // If we have path to a directory...
            if (pathToDirectory != null)
            {
                // Load the directory of the path
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(pathToDirectory);
            }

            // If current page is not up to date
            if (CurrentPage != page)
            {
                // Update page
                CurrentPage = page;
                // Raise new_page_requested_event
                OnNewPageRequested();
            }
        }

        public void NavigateToNextPage(string? pagePath = null)
        {
            //CurrentPage = NextPage;

            //UpdateNavigatedPageHistory();

            //OnNewPageRequested();
        }

        public void NavigateToPreviousPage(string? pagePath = null)
        {
            if (NavigatedPageHistory.Count - 1 == 1)
                return;

            if(NavigatedPageCounter == 0)
                NavigatedPageCounter = NavigatedPageHistory.Count;

            PreviousPage = NavigatedPageHistory[NavigatedPageCounter - 2];

            CurrentPage = PreviousPage.Item1;

            if (!string.IsNullOrEmpty(PreviousPage.Item2))
            {
                ServiceLocator.DirectoryExplorerVM.LoadDirectoryItems(PreviousPage.Item2);
            }

            NavigatedPageCounter--;

            //UpdateNavigatedPageHistory();

            OnNewPageRequested();
        }

        public void UpdateNavigatedPageHistory(ApplicationPages page, string currentPagePath)
        {
            if (!(NavigatedPageHistory[NavigatedPageHistory.Count - 1].Equals((page, currentPagePath))))
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
