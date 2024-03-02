using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shell;

namespace File.Manager
{
    /// <summary>
    /// Custom main window
    /// </summary>
    public class CustomMainWindow : Window
    {
        /// <summary>
        /// Window handle
        /// </summary>
        private IntPtr _handle;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomMainWindow()
        {
            // Initialize source
            SourceInitialized += (sender, e) => _handle = new WindowInteropHelper(this).Handle; 
            // Apply pre-defined style to this window
            Style = (Style)TryFindResource("CustomMainWindowStyle");

            // Create window chrome object
            var chrome = new WindowChrome()
            {
                ResizeBorderThickness = WindowState == WindowState.Maximized ? new Thickness(0) : new Thickness(8),
                GlassFrameThickness = new Thickness(0),
                CornerRadius = new CornerRadius(0),
            };

            // Set this window's window chrome
            WindowChrome.SetWindowChrome(this, chrome);
        }

        /// <summary>
        /// Applies defined funcitonality to the custom window's template
        /// </summary>
        public override void OnApplyTemplate()
        {
            // Call base
            base.OnApplyTemplate();

            // flag used to track when user is dragging this window from a maximized state.
            var windowRestore = false;

            // Get template elements
            var captionArea = GetTemplateChild("PART_CaptionArea") as FrameworkElement;
            var sideMenuCaptionArea = GetTemplateChild("PART_SideMenuCaptionArea") as FrameworkElement;
            var windowLimitMargin = GetTemplateChild("PART_WindowLimitMargin") as FrameworkElement;
            var customWindow = GetTemplateChild("PART_CustomWindow") as FrameworkElement;

            // caption area element isn't null
            if (captionArea != null )
            {
                // Hook into left mouse down event
                captionArea.MouseLeftButtonDown += (sender, e) =>
                {
                    // TODO: support double click on laptop track pad

                    // check if user double clicked the title bar
                    if (e.ClickCount.Equals(2))
                        // Switch between normal and maximized states when user double clicks the title bar.
                        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    // Otherwise...
                    else 
                    {
                        // If window is maximized
                        if (WindowState == WindowState.Maximized)
                            // Set flag to true
                            windowRestore = true;

                        // Drag window
                        DragMove();
                    }

                    // Reset windowRestore flag when left mouse button is up
                    captionArea.MouseLeftButtonUp += (sender, e) => { windowRestore = false; };

                    // Hook into mouse move event
                    captionArea.MouseMove += (sender, e) =>
                    {
                        // If we are to restore window from a maximized state
                        if (windowRestore)
                        {
                            // Reset flag
                            windowRestore = false;

                            // Get mouse position 
                            var mousePos = e.GetPosition(this).X;
                            // Get bounds width
                            var restoreBoundsWidth = RestoreBounds.Width;
                            var newWindowPos = mousePos - restoreBoundsWidth / 2;

                            if (newWindowPos < 0)
                                newWindowPos = 0;
                            else if(newWindowPos + restoreBoundsWidth > SystemParameters.WorkArea.Width)
                                newWindowPos = SystemParameters.WorkArea.Width - restoreBoundsWidth;

                            WindowState = WindowState.Normal;
                            Left = newWindowPos;
                            Top = 0;
                            DragMove();
                        }
                    };

                };
            }

            // side Menu caption area element isn't null
            if (sideMenuCaptionArea != null)
            {
                // Hook into left mouse down event
                sideMenuCaptionArea.MouseLeftButtonDown += (sender, e) =>
                {
                    // check if user double clicked the title bar
                    if (e.ClickCount.Equals(2))
                        // Switch between normal and maximized states when user double clicks the title bar.
                        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    // Otherwise...
                    else
                    {
                        // If window is maximized
                        if (WindowState == WindowState.Maximized)
                            // Set flag to true
                            windowRestore = true;

                        // Drag window
                        DragMove();
                    }

                    // Reset windowRestore flag when left mouse button is up
                    sideMenuCaptionArea.MouseLeftButtonUp += (sender, e) => { windowRestore = false; };

                    // Hook into mouse move event
                    sideMenuCaptionArea.MouseMove += (sender, e) =>
                    {
                        // If we are to restore window from a maximized state
                        if (windowRestore)
                        {
                            // Reset flag
                            windowRestore = false;

                            // Get mouse position 
                            var mousePos = e.GetPosition(this).X;
                            // Get bounds width
                            var restoreBoundsWidth = RestoreBounds.Width;

                            var newWindowPos = mousePos - restoreBoundsWidth / 2;

                            if (newWindowPos < 0)
                                newWindowPos = 0;
                            else if (newWindowPos + restoreBoundsWidth > SystemParameters.WorkArea.Width)
                                newWindowPos = SystemParameters.WorkArea.Width - restoreBoundsWidth;

                            WindowState = WindowState.Normal;
                            Left = newWindowPos;
                            Top = 0;
                            DragMove();
                        }

                    };

                };
            }

            // If we have window limit element...
            if (windowLimitMargin != null)
            {
                // Monitor when size of this window changes
                SizeChanged += (sender, e) =>
                {
                    // Then calculate margin of this window limit element when this window is maximized
                    windowLimitMargin.Margin = WindowState == WindowState.Maximized ? GetMaximizedMarginThickness() : new Thickness(0);
                };
            }

        }

        /// <summary>
        /// Calculates the right margin value, used to keep this window from being too large
        /// </summary>
        /// <returns><see cref=" Thickness"/></returns>
        private Thickness GetMaximizedMarginThickness()
        {
            var windowLimitMargin = GetTemplateChild("PART_WindowLimitMargin") as FrameworkElement;
            if (windowLimitMargin == null)
                return new Thickness(0);

            Rect area = SystemParameters.WorkArea;
            Point detectorCorner = windowLimitMargin.PointToScreen(new Point(0, 0));

            PresentationSource presentationSource = PresentationSource.FromVisual(this);
            double dpiScaleX = presentationSource.CompositionTarget.TransformToDevice.M11;
            double dpiScaleY = presentationSource.CompositionTarget.TransformToDevice.M22;

            double areaRight = area.Width / dpiScaleX;
            double areaBottom = area.Height / dpiScaleY;
            double offsetX = area.Left - detectorCorner.X;
            double offsetY = area.Top - detectorCorner.Y;

            double top = offsetX / dpiScaleX;
            double left = offsetY / dpiScaleY;
            double right = (windowLimitMargin.ActualWidth - offsetX - areaRight) / dpiScaleX;
            double bottom = (windowLimitMargin.ActualHeight - offsetY - areaBottom) / dpiScaleY;

            return new Thickness(left, top, right, bottom);
        }
    }
}
