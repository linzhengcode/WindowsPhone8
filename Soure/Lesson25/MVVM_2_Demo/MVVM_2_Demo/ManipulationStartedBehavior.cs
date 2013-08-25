using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_2_Demo
{
    public static class ManipulationStartedBehavior
    {
        public static readonly DependencyProperty ManipulationStartedCommandProperty=
            DependencyProperty.RegisterAttached(
            "ManipulationStartedCommand",
             typeof(ICommand),
             typeof(ManipulationStartedBehavior),
             new PropertyMetadata(null, new PropertyChangedCallback(ManipulationStartedPropertyChangedCallback)));

        public static ICommand GetManipulationStartedCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ManipulationStartedCommandProperty);
        }

        public static void SetManipulationStartedCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(ManipulationStartedCommandProperty, value);
        }

        private static void ManipulationStartedPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement uiElement = (UIElement)d;
            uiElement.ManipulationStarted += uiElement_ManipulationStarted;
            uiElement.ManipulationCompleted += uiElement_ManipulationCompleted;
            uiElement.ManipulationDelta += uiElement_ManipulationDelta;
        }

        static void uiElement_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //throw new NotImplementedException();
        }

        static void uiElement_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        static void uiElement_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            UIElement uiElement = (UIElement)sender;
            GetManipulationStartedCommand(uiElement).Execute(e.ManipulationOrigin);
        }

    }
}
