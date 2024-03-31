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
    /// Interaction logic for SideMenuItemCollectionControl.xaml
    /// </summary>
    public partial class SideMenuItemCollectionControl : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SideMenuItemCollectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The header of this collection of directory items
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(SideMenuItemCollectionControl), new PropertyMetadata(defaultValue: string.Empty));

        /// <summary>
        /// True if elements contained in this control are quick access directories
        /// otherwise, false if not.
        /// default value = false
        /// </summary>
        public bool IsQuickAccessCollection
        {
            get { return (bool)GetValue(IsQuickAccessCollectionProperty); }
            set { SetValue(IsQuickAccessCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsQuickAccessCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsQuickAccessCollectionProperty =
            DependencyProperty.Register("IsQuickAccessCollection", typeof(bool), typeof(SideMenuItemCollectionControl), new PropertyMetadata(defaultValue: false));



    }
}
