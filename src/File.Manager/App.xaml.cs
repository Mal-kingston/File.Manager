using System.ComponentModel;
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

            // Used to scan for external drives / devices
            USBInterfaceMonitor.ScanForAvailableDrives(this, new DoWorkEventArgs(default));

            // Create main window and set it's data context
            var mainAppWindow = new MainWindow { DataContext = new MainWindowViewModel() };
            // Set app main window to main-app-window 
            Current.MainWindow = mainAppWindow;
            // Show window
            Current.MainWindow.Show();

        }

    }
}
