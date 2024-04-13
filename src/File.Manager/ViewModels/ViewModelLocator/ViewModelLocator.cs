namespace File.Manager
{
    /// <summary>
    /// View model locator of this application
    /// </summary>
    public class ViewModelLocator 
    {
        /// <summary>
        /// A navigation service
        /// </summary>
        public static INavigationService NavigationService { get; set; }

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel AppViewModel { get; }

        /// <summary>
        /// The <see cref="HomePage"/> view model
        /// </summary>
        public static HomePageViewModel HomePageViewModel { get; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static ViewModelLocator()
        {
            // Initialize objects
            NavigationService = new NavigationService();
            AppViewModel = new ApplicationViewModel(NavigationService);
            HomePageViewModel = new HomePageViewModel();

            // TODO: Make a view model factory
        }

    }
}
