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
    public class Behaviors
    {
        public static readonly DependencyProperty DropFileCommandProperty =
            DependencyProperty.RegisterAttached("DropFileCommand", typeof(ICommand),
            typeof(Behaviors), new UIPropertyMetadata(
                default(ICommand),
                new PropertyChangedCallback(DropFileCommandChanged)
                ));

        public static void DropFileCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            element.Drop += Element_DropFile;
        }

        private static void Element_DropFile(object sender, DragEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetDropFileCommand(element);

            command.Execute(e);
        }

        public static void SetDropFileCommand(UIElement element, ICommand value)
        {
            element.SetValue(DropFileCommandProperty, value);
        }

        public static ICommand GetDropFileCommand(UIElement element)
        {
            return (ICommand)element.GetValue(DropFileCommandProperty);
        }
    }
    
}
