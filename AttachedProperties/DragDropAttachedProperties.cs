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
    /// Enables dragging and dropping files onto any framework element
    /// </summary>
    public class DragDrop
    {
        /// <summary>
        /// Registers the new attached property
        /// </summary>
        public static readonly DependencyProperty DropFileCommandProperty =
            DependencyProperty.RegisterAttached("DropFileCommand", typeof(ICommand),
            typeof(DragDrop), new UIPropertyMetadata(
                default(ICommand),
                new PropertyChangedCallback(DropFileCommandChanged)
                ));

        /// <summary>
        /// Fires when a new command is set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DropFileCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            element.PreviewDragOver += element_PreviewDragOver;
            element.Drop += Element_DropFile;
        }

        /// <summary>
        /// handles the preview drag over for textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void element_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Calls the command bound to the attached property (see viewmodel)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Element_DropFile(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            
            ICommand command = GetDropFileCommand(element);

            command.Execute(e);
        }

        /// <summary>
        /// the setter for attached property
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetDropFileCommand(UIElement element, ICommand value)
        {
            element.SetValue(DropFileCommandProperty, value);
        }

        /// <summary>
        /// The getter for attached property
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static ICommand GetDropFileCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DropFileCommandProperty);
        }
    }
    
}
