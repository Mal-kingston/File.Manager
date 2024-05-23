using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File.Manager
{
    /// <summary>
    /// Sets focus on <see cref="FrameworkElement"/> when they are visible
    /// </summary>
    public class FocusAttachedProperties : AttachedPropertyBase<FocusAttachedProperties, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the control
            var control = (FrameworkElement)sender;

            // Make sure it's not null
            if (control != null)
            {
                // Hook into control visibility changing 
                control.IsVisibleChanged += (s, e) =>
                {
                    // If control is visible
                    if (control.IsVisible)
                        // Set focus on it
                        control.Focus();
                };
            }

        }
    }
}
