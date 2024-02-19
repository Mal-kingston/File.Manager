using System.Configuration;
using System.Data;
using System.Windows;

namespace File.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainAppWindow = new MainWindow { DataContext = new MainWindowViewModel() };
            Current.MainWindow = mainAppWindow;
            Current.MainWindow.Show();

        }
    }

}
