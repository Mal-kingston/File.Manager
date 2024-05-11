using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace File.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Manually start this application 
        /// </summary>
        /// <param name="e">Start up event </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize and add dependencies
            DependencyInjection.InitializeDependencyInjection()
                               .AddApplicationView()
                               .AddViewModels()
                               .AddServices();

            // Build injected dependency
            DependencyInjection.Build();

            // Set default page
            ServiceLocator.NavigationService.NavigateToPage(ApplicationPages.Home);
            // Set default page path
            ServiceLocator.NavigationBarVM.SetNavigatedDirectoryPath("Home");

            // Get main window
            var mainAppWindow = DependencyInjection.GetDependency<MainWindow>();

            // Make sure we have main window
            if (mainAppWindow != null)
            {
                // Set main window data context
                mainAppWindow.DataContext = new MainWindowViewModel();
                // Set app main window to main-app-window 
                Current.MainWindow = mainAppWindow;
                // Show window
                Current.MainWindow.Show();
            }

            // Used to scan for external drives / devices
            USBInterfaceMonitor.ScanForAvailableDrives(this, new DoWorkEventArgs(default));

        }

    }
}
