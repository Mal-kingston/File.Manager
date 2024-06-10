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
        public static HomePageViewModel HomePageVM => DependencyInjection.GetDependency<HomePageViewModel>();

        /// <summary>
        /// The <see cref="SideMenuControlViewModel"/> view model
        /// </summary>

        public static SideMenuControlViewModel SideMenuControlVM => DependencyInjection.GetDependency<SideMenuControlViewModel>();

        /// <summary>
        /// The <see cref="DirectoryExplorer"/> view model
        /// </summary>
        public static DirectoryExplorerViewModel DirectoryExplorerVM => DependencyInjection.GetDependency<DirectoryExplorerViewModel>();

        /// <summary>
        /// The <see cref="NavigationBarControl"/> view model
        /// </summary>
        public static NavigationBarViewModel NavigationBarVM => DependencyInjection.GetDependency<NavigationBarViewModel>();

        /// <summary>
        /// The <see cref="SideMenuItemControlViewModel"/> view model
        /// </summary>
        public static SideMenuItemControlViewModel SideMenuItemControlVM => DependencyInjection.GetDependency<SideMenuItemControlViewModel>();

        /// <summary>
        /// The <see cref="DevicesAndDrivesViewModel"/> view model
        /// </summary>
        public static DevicesAndDrivesViewModel DevicesAndDrivesVM => DependencyInjection.GetDependency<DevicesAndDrivesViewModel>();

    }
}
