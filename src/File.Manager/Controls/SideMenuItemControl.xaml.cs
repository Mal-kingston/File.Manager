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
    /// Interaction logic for SideMenuItemControl.xaml
    /// </summary>
    public partial class SideMenuItemControl : UserControl
    {
        public SideMenuItemControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Shows or hide tag on sub menu item 
        /// </summary>
        public bool ShowTag
        {
            get { return (bool)GetValue(ShowTagProperty); }
            set { SetValue(ShowTagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowTag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowTagProperty =
            DependencyProperty.Register("ShowTag", typeof(bool), typeof(SideMenuItemControl), new PropertyMetadata(defaultValue: true));

    }
}
