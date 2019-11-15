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
    public class DragDropProperty : BaseAttachedProperty<DragDropProperty, ICommand>
    {
        /// <summary>
        /// Fires when a new command is set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            
            ICommand command = GetValue(element);

            command.Execute(e);
        }
        
    }
    
}
