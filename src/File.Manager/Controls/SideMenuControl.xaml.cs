using System;
using System.Collections.Generic;
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
    /// Interaction logic for SideMenuControl.xaml
    /// </summary>
    public partial class SideMenuControl : UserControl
    {
        public SideMenuControl()
        {
            InitializeComponent();

            // Set data context
            DataContext = ServiceLocator.SideMenuControlVM;

        }

        /// <summary>
        /// Scroll viewer event 
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e">The event argument</param>
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // If scroll bar is visible...
            if (ScrollViewer.ComputedVerticalScrollBarVisibility.Equals(Visibility.Visible))
                // Make border visible
                RightBorder.Visibility = Visibility.Visible;
            else 
                // Otherwise collapse border
                RightBorder.Visibility = Visibility.Collapsed;
        }

    }
}
