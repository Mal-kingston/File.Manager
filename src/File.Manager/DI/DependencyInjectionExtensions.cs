using Microsoft.Extensions.DependencyInjection;

namespace File.Manager
{
    /// <summary>
    /// Dependency injection extension methods
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds views as a dependency
        /// </summary>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApplicationView(this IServiceCollection services)
        {
            // Views added
            services.AddSingleton<DrivesAndDevicesPage>();
            services.AddSingleton<DirectoryExplorer>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<HomePage>();

            // Return services
            return services;
        }

        /// <summary>
        /// Adds view models as a dependency
        /// </summary>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            // View-models added
            services.AddSingleton(provider => 
            {
                var navigationService = provider.GetService<INavigationService>();
                if (navigationService == null) throw new Exception($"Requested service {navigationService} not found");
                return new ApplicationViewModel(navigationService);
            });
            services.AddSingleton<SideMenuItemControlViewModel>();
            services.AddSingleton<DirectoryExplorerViewModel>();
            services.AddSingleton<SideMenuControlViewModel>();
            services.AddSingleton<NavigationBarViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<HomePageViewModel>();

            // Return services
            return services;
        }

        /// <summary>
        /// Adds services as a dependency
        /// </summary>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // _service added
            services.AddSingleton<INavigationService>(new NavigationService());
            
            // Return services
            return services;
        }

    }
}
