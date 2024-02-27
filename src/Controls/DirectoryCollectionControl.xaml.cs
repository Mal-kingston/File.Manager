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
    /// Interaction logic for DirectoryCollectionControl.xaml
    /// </summary>
    public partial class DirectoryCollectionControl : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryCollectionControl()
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
            DependencyProperty.Register("Header", typeof(string), typeof(DirectoryCollectionControl), new PropertyMetadata(defaultValue: string.Empty));

        /// <summary>
        /// True if elements contained in this control are quick access directories
        /// otherwise, false if not.
        /// default value = false
        /// </summary>
        public bool IsParentToQuickAccessDirectories
        {
            get { return (bool)GetValue(IsParentToQuickAccessDirectoriesProperty); }
            set { SetValue(IsParentToQuickAccessDirectoriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsParentToQuickAccessDirectories.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsParentToQuickAccessDirectoriesProperty =
            DependencyProperty.Register("IsParentToQuickAccessDirectories", typeof(bool), typeof(DirectoryCollectionControl), new PropertyMetadata(defaultValue: false));

    }
}
