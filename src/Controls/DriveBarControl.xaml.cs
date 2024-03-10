using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DriveOverviewControl.xaml
    /// </summary>
    public partial class DriveOverviewControl : UserControl
    {
        public DriveOverviewControl()
        {
            InitializeComponent();
            // Wait for this control to load
            Loaded += (sender, e) =>
            {
                // set space options
                DriveUsedSpace = UsePercentage ? string.Format($"{CurrentValue}%") : DriveUsedSpace;
                DriveAvailableSpace = UsePercentage ? string.Format($"{(ProgressIndicator.Maximum - CurrentValue)}%") : DriveAvailableSpace;
            };
        }

        /// <summary>
        /// Current value of the custom progress bar
        /// </summary>
        public double CurrentValue
        {
            get { return (double)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(DriveOverviewControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// The used space size of a drive
        /// </summary>
        public string DriveUsedSpace
        {
            get { return (string)GetValue(DriveUsedSpaceProperty); }
            set { SetValue(DriveUsedSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DriveUsedSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DriveUsedSpaceProperty =
            DependencyProperty.Register(nameof(DriveUsedSpace), typeof(string), typeof(DriveOverviewControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The available space size of a drive
        /// </summary>
        public string DriveAvailableSpace
        {
            get { return (string)GetValue(DriveAvailableSpaceProperty); }
            set { SetValue(DriveAvailableSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DriveAvailableSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DriveAvailableSpaceProperty =
            DependencyProperty.Register("DriveAvailableSpace", typeof(string), typeof(DriveOverviewControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// The total size of a drive
        /// </summary>
        public string TotalDriveSize
        {
            get { return (string)GetValue(TotalDriveSizeProperty); }
            set { SetValue(TotalDriveSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalDriveSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalDriveSizeProperty =
            DependencyProperty.Register("TotalDriveSize", typeof(string), typeof(DriveOverviewControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// True if percentage value should be used to indicate the percentages of used and un-used spaces in this control
        /// Otherwise, false if the actual size should be displayed
        /// </summary>
        public bool UsePercentage
        {
            get { return (bool)GetValue(UsePercentageProperty); }
            set { SetValue(UsePercentageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UsePercentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePercentageProperty =
            DependencyProperty.Register("UsePercentage", typeof(bool), typeof(DriveOverviewControl), new PropertyMetadata(default(bool)));

    }
}
