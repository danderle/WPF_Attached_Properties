using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrypticApp
{
    /// <summary>
    /// Matches the label width of all the custom <see cref="HashListItemControls"/> inside this panel
    /// </summary>
    public class WidthMatcherProperty : BaseAttachedProperty<WidthMatcherProperty, bool>
    {

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Get the panel
            var panel = (sender as Panel);

            //Call SetWidth initially (helps for design time)
            SetWidths(panel);

            //Wait for panel to load
            RoutedEventHandler onLoaded = null;

            onLoaded += (s, ee) =>
            {
                //Unhook
                panel.Loaded -= onLoaded;

                //Set all widths
                SetWidths(panel);

                //Loop the panels children
                foreach(var child in panel.Children)
                {
                    //Ignore any non HashListItemControls
                    if (!(child is HashListItemControl))
                        continue;

                    var control = (child as UserControl);
                    var hashControl = (control as HashListItemControl);
                    if (hashControl == null)
                        return;

                    //call sizechanged callback and Set the widths
                    hashControl.Label.SizeChanged += (ss, eee) =>
                    {
                        //Update widths
                        SetWidths(panel);
                    };
                }
            };

            //Hook into the Loaded event
            panel.Loaded += onLoaded;
        }

        /// <summary>
        /// Update all child HashListItemControls
        /// <param name="panel">The panel containing the <see cref="HashListItemControls"/></param>
        /// </summary>
        private void SetWidths(Panel panel)
        {
            //Keep track of maximum size width
            var maxSize = 0d;

            //Loop the panels children to find the largest value
            foreach (var child in panel.Children)
            {
                //Ignore any non HashListItemControls
                if (!(child is HashListItemControl))
                    continue;
                var control = (child as HashListItemControl);

                //Finds the largest value
                maxSize = Math.Max(maxSize, control.Label.RenderSize.Width + control.CheckBox.Margin.Left + control.CheckBox.Margin.Right);
            }

            var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());

            //Loop the panels children to set the largest value
            foreach (var child in panel.Children)
            {
                //Ignore any non HashListItemControls
                if (!(child is HashListItemControl))
                    continue;
                var control = (child as UserControl);
                var hashControl = (control as HashListItemControl);
                if (hashControl == null)
                    return;
                //Set each control LabelWidth value to the maxSize
                hashControl.LabelWidth = gridLength;
            }
        }
    }
    
}
