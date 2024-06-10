using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File.Manager
{
    /// <summary>
    /// Interaction logic for NavigationBarControl.xaml
    /// </summary>
    public partial class NavigationBarControl : UserControl
    {
        public NavigationBarControl()
        {
            InitializeComponent();

            // Set Data context            
            DataContext = ServiceLocator.NavigationBarVM;
        }

        /// <summary>
        /// Scroll to the latest navigated path
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e">Event args</param>
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e) => ((ScrollViewer)sender).ScrollToRightEnd();
        
    }
}
