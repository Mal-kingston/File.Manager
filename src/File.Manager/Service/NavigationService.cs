namespace File.Manager
{
    /// <summary>
    /// Service that facilitates navigation in this application
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The page that is currently in view
        /// </summary>
        ApplicationPages CurrentPage { get; set; }

        /// <summary>
        /// Event to fire when a new page is requested
        /// </summary>
        event EventHandler<object>? NewPageRequested;

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        void NavigateToPage(ApplicationPages page);
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
        /// Event to fire when a new page is requested
        /// </summary>
        public event EventHandler<object>? NewPageRequested;

        /// <summary>
        /// Navigates to a specified page
        /// </summary>
        /// <param name="page">The specific page to navigate to</param>
        public void NavigateToPage(ApplicationPages page)
        {
            // If current page is not up to date
            if(CurrentPage != page)
                // Update page
                CurrentPage = page;
            // Raise new_page_requested_event
            OnNewPageRequested();
        }

        /// <summary>
        /// Raises the <see cref="NewPageRequested"/> event
        /// </summary>
        public virtual void OnNewPageRequested()
        {
            NewPageRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
