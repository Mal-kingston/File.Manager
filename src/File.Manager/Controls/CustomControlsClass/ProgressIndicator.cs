using System.Windows;
using System.Windows.Controls;

namespace File.Manager
{
    /// <summary>
    /// Custom <see cref="ProgressBar"/> control
    /// <remark>
    /// This control is made so that values of both used and unused spaces
    /// in progress bar could be displayed to user
    /// </remark>
    /// </summary>
    public class ProgressIndicator : Control
    {
        #region Private Fields

        /// <summary>
        /// The range of this control
        /// </summary>
        private FrameworkElement _range;

        /// <summary>
        /// The indicator representing the portion of used space in this control
        /// </summary>
        private FrameworkElement _usedSpaceIndicator;

        /// <summary>
        /// The indicator representing the portion of unused space in this control
        /// </summary>
        private FrameworkElement _unUsedSpaceIndicator;

        /// <summary>
        /// The application main window
        /// </summary>
        private Window AppWindow => Application.Current.MainWindow;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgressIndicator()
        {
            // Set defaults
            _range = new FrameworkElement();
            _usedSpaceIndicator = new FrameworkElement();
            _unUsedSpaceIndicator = new FrameworkElement();
            Maximum = 100;
            Minimum = 0;

            // Adjust values when window changes it's size or state
            //Application.Current.MainWindow.SizeChanged += (sender, e) => SetIndicatorsValue();

            // If window is not null...
            if(AppWindow != null )
            // Adjust values when window changes it's size or state
                AppWindow.SizeChanged += (sender, e) => SetIndicatorsValue();
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// The current value of this control specifying the 
        /// portion of the currently used space 
        /// <remark>Percentage based value</remark>
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ProgressIndicator), new PropertyMetadata(default(double), OnValueChanged));

        /// <summary>
        /// The maximum limit of this control
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(ProgressIndicator), new PropertyMetadata(default(double), OnMaximumValueChanged));

        /// <summary>
        /// The minimum limit of this control
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(ProgressIndicator), new PropertyMetadata(default(double), OnMinimumValueChanged));

        /// <summary>
        /// The value specifying the portion of the used space of this control
        /// <remark>Percentage base value</remark>
        /// </summary>
        public string UsedSpace
        {
            get { return (string)GetValue(UsedSpaceProperty); }
            set { SetValue(UsedSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UsedSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsedSpaceProperty =
            DependencyProperty.Register("UsedSpace", typeof(string), typeof(ProgressIndicator), new PropertyMetadata(default(string)));


        /// <summary>
        /// The value specifying the portion of the un-used space of this control
        /// <remark>Percentage base value</remark>
        /// </summary>
        public string UnUsedSpace
        {
            get { return (string)GetValue(UnUsedSpaceProperty); }
            set { SetValue(UnUsedSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UnUsedSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnUsedSpaceProperty =
            DependencyProperty.Register("UnUsedSpace", typeof(string), typeof(ProgressIndicator), new PropertyMetadata(default(string)));

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
            DependencyProperty.Register("UsePercentage", typeof(bool), typeof(ProgressIndicator), new PropertyMetadata(default(bool)));

        #endregion

        #region Method Overrides

        /// <summary>
        /// Override this method to set / capture our control template elements
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // Set fields
            _range = (FrameworkElement)GetTemplateChild("Range");
            _usedSpaceIndicator = (FrameworkElement)GetTemplateChild("UsedSpaceIndicator");
            _unUsedSpaceIndicator = (FrameworkElement)GetTemplateChild("UnUsedSpaceIndicator");

            // Make sure our fields are not null
            if (_usedSpaceIndicator != null && _unUsedSpaceIndicator != null)
            {
                // Hook into size changed events
                _usedSpaceIndicator.SizeChanged += OnSizeChanged;
                _unUsedSpaceIndicator.SizeChanged += OnSizeChanged;
            }
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Re-sets the indicators sizes whenever size changes
        /// </summary>
        /// <param name="sender">The origin of this event</param>
        /// <param name="e"></param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Un-hook events
            _usedSpaceIndicator.SizeChanged -= OnSizeChanged;
            _unUsedSpaceIndicator.SizeChanged -= OnSizeChanged;

            // Set size values
            SetIndicatorsValue();
        }

        /// <summary>
        /// Called whenever value changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressIndicator = (ProgressIndicator)d;
            progressIndicator.SetIndicatorsValue();
        }

        /// <summary>
        /// Called whenever value changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMaximumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressIndicator = (ProgressIndicator)d;
            progressIndicator.SetIndicatorsValue();
        }

        /// <summary>
        /// Called whenever value changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMinimumValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressIndicator = (ProgressIndicator)d;
            progressIndicator.SetIndicatorsValue();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates and sets the indicator values
        /// </summary>
        private void SetIndicatorsValue()
        {
            // Percentage
            double percent = (Value - Minimum) / (Maximum - Minimum);
            // Get values
            double progressValue = percent * _range.ActualWidth;
            double unUsedSpaceValue = ((Maximum / Maximum) - percent) * _range.ActualWidth;

            // Make sure we have valid values
            if(progressValue >= 0 && unUsedSpaceValue >= 0)
            {
                // Set both used  and un-used indicator actual values [percentage based values]
                _usedSpaceIndicator.Width = progressValue;
                _unUsedSpaceIndicator.Width = unUsedSpaceValue;
            }
        }

        #endregion
    }
}
