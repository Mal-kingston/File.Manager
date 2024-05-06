using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Interaction logic for DirectoryItemControl.xaml
    /// </summary>
    public partial class DirectoryItemControl : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryItemControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This directory icon
        /// </summary>
        public IconType Icon
        {
            get { return (IconType)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(IconType), typeof(DirectoryItemControl), new PropertyMetadata(default(IconType)));

        /// <summary>
        /// This directory name
        /// </summary>
        public string DirectoryName
        {
            get { return (string)GetValue(DirectoryNameProperty); }
            set { SetValue(DirectoryNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectoryName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectoryNameProperty =
            DependencyProperty.Register("DirectoryName", typeof(string), typeof(DirectoryItemControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The latest date and time this directory was modified
        /// </summary>
        public string LastModifyDateTime
        {
            get { return (string)GetValue(LastModifyDateTimeProperty); }
            set { SetValue(LastModifyDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastModifyDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastModifyDateTimeProperty =
            DependencyProperty.Register("LastModifyDateTime", typeof(string), typeof(DirectoryItemControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The type of this directory item
        /// </summary>
        public string FileType
        {
            get { return (string)GetValue(FileTypeProperty); }
            set { SetValue(FileTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileTypeProperty =
            DependencyProperty.Register("FileType", typeof(string), typeof(DirectoryItemControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The size of this directory item
        /// </summary>
        public string FileSize
        {
            get { return (string)GetValue(FileSizeProperty); }
            set { SetValue(FileSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileSizeProperty =
            DependencyProperty.Register("FileSize", typeof(string), typeof(DirectoryItemControl), new PropertyMetadata(default(string)));

    }
}
