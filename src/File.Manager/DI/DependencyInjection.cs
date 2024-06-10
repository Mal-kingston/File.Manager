using Microsoft.Extensions.DependencyInjection;

namespace File.Manager
{
    /// <summary>
    /// Dependency injection 
    /// </summary>
    public class DependencyInjection
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider _serviceProvider = default!;

        /// <summary>
        /// The services
        /// </summary>
        private static IServiceCollection _service = default!;

        /// <summary>
        /// Initializes the dependency injection 
        /// </summary>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection InitializeDependencyInjection()
        {
            // Create service collection
            _service = new ServiceCollection();

            // Return service collection instance
            return _service;
        }

        /// <summary>
        /// Builds the injected service collection 
        /// </summary>
        /// <returns>Dependencies or services injected into <see cref="ServiceCollection"/></returns>
        public static IServiceProvider Build()
        {
            // Build and set service provider
            _serviceProvider = _service.BuildServiceProvider();

            // Dependencies / services injected
            return _serviceProvider;
        }

        /// <summary>
        /// Provides requested service or dependency
        /// </summary>
        /// <typeparam name="T">The service of dependency to provide</typeparam>
        /// <returns>An instance of service or dependency requested</returns>
        public static T GetDependency<T>() where T : notnull
        {
            // Return service requested
            return _serviceProvider.GetService<T>()!;
        }
    }
}
