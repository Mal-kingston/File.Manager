using Microsoft.Extensions.DependencyInjection;

namespace File.Manager
{
    /// <summary>
    /// View model locator of this application
    /// </summary>
    public class ServiceLocator 
    {
        /// <summary>
        /// A navigation service
        /// </summary>
        public static INavigationService NavigationService => DependencyInjection.GetDependency<INavigationService>();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel AppViewModel => DependencyInjection.GetDependency<ApplicationViewModel>();

        /// <summary>
        /// The <see cref="HomePage"/> view model
        /// </summary>
        public static HomePageViewModel HomePageViewModel => DependencyInjection.GetDependency<HomePageViewModel>();

        /// <summary>
        /// The <see cref="SideMenuControlViewModel"/> view model
        /// </summary>

        public static SideMenuControlViewModel SideMenuControlViewModel => DependencyInjection.GetDependency<SideMenuControlViewModel>();

        /// <summary>
        /// The <see cref="DirectoryExplorerViewModel "/> view model
        /// </summary>
        public static DirectoryExplorerViewModel DirectoryExplorerViewModel => DependencyInjection.GetDependency<DirectoryExplorerViewModel>();

    }
}
