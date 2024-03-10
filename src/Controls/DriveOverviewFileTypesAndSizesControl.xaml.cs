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
    /// Interaction logic for DriveOverviewFileTypesAndSizesControl.xaml
    /// </summary>
    public partial class DriveOverviewFileTypesAndSizesControl : UserControl
    {
        public DriveOverviewFileTypesAndSizesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The icon that indicates the type of file this control represents
        /// </summary>
        public IconType Icon
        {
            get { return (IconType)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(IconType), typeof(DriveOverviewFileTypesAndSizesControl), new PropertyMetadata(default(IconType)));

        /// <summary>
        /// The name or group name of files of this kind
        /// </summary>
        public string FileTypeName
        {
            get { return (string)GetValue(FileTypeNameProperty); }
            set { SetValue(FileTypeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileTypeNameProperty =
            DependencyProperty.Register("FileTypeName", typeof(string), typeof(DriveOverviewFileTypesAndSizesControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The size of space occupied by this file type in the drive storage
        /// </summary>
        public string OccupiedSpaceSize
        {
            get { return (string)GetValue(OccupiedSpaceSizeProperty); }
            set { SetValue(OccupiedSpaceSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OccupiedSpaceSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OccupiedSpaceSizeProperty =
            DependencyProperty.Register("OccupiedSpaceSize", typeof(string), typeof(DriveOverviewFileTypesAndSizesControl), new PropertyMetadata(default(string)));

    }
}
