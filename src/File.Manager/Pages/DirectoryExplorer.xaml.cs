using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
    /// Interaction logic for DirectoryExplorer.xaml
    /// </summary>
    public partial class DirectoryExplorer : Page
    {
        public DirectoryExplorer()
        {
            InitializeComponent();

            // Data-context
            DataContext = ServiceLocator.DirectoryExplorerVM;
        }

        /// <summary>
        /// Pop-up control visibility changed event 
        /// 
        /// <Remarks>
        /// This is used to set to location of where pop-up control should appear.
        /// Ideal location is set to be anywhere within app window 
        /// </Remarks>
        /// </summary>
        /// <param name="sender">Origin of this event</param>
        /// <param name="e">Event args</param>
        private void DirectoryItemPopupControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Focus on pop-up control
            PopupControl.Focus();

            // Get mouse location relative to pop up container
            Point mousePointerPosition = Mouse.GetPosition(PopupControlContainer);

            // Account for bottom right corner
            if (DirectoryItem.ActualWidth - mousePointerPosition.X < PopupControl.Width && DirectoryItem.ActualHeight - mousePointerPosition.Y < PopupControl.Height)
            {
                Canvas.SetLeft(PopupControl, (mousePointerPosition.X - 1) - PopupControl.Width);
                Canvas.SetTop(PopupControl, (mousePointerPosition.Y - 1) - PopupControl.Height);
            }
            // Account for right edge
            else if (DirectoryItem.ActualWidth - mousePointerPosition.X < PopupControl.Width)
            {
                Canvas.SetLeft(PopupControl, (mousePointerPosition.X - 1) - PopupControl.Width);
                Canvas.SetTop(PopupControl, (mousePointerPosition.Y - 1));
            }
            // Account for top edge
            else if (DirectoryItem.ActualHeight - mousePointerPosition.Y < PopupControl.Height)
            {
                Canvas.SetLeft(PopupControl, (mousePointerPosition.X + 1));
                Canvas.SetTop(PopupControl, (mousePointerPosition.Y + 1) - PopupControl.Height);
            }
            // If pop up wont overflow, set position
            else
            {
                Canvas.SetLeft(PopupControl, (mousePointerPosition.X + 1));
                Canvas.SetTop(PopupControl, (mousePointerPosition.Y + 1));
            }
        }
    }
}
