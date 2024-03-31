using System.Windows;
using System.Windows.Media.Effects;

namespace File.Manager
{
    /// <summary>
    /// Drop shadow attached property
    /// </summary>
    public class DropShadowAttachedProperty : AttachedPropertyBase<DropShadowAttachedProperty, bool>
    {
        /// <summary>
        /// Override on-value-changed event
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get sender as framework element
            var element = (sender as FrameworkElement);

            // Make sure element isn't null
            if (element != null)
            {
                // Hook into element loading
                element.Loaded += (sender, e) =>
                {
                    // Define element drop shadow
                    element.Effect = new DropShadowEffect
                    {
                        ShadowDepth = 2,
                        BlurRadius = 4,
                        Opacity = 0.1,
                        RenderingBias = RenderingBias.Quality,
                    };
                };
            }

        }
    }
}
