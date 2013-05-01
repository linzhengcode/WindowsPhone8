using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyMessageBoxDemo
{
    public class MyMessageBox
    {
        Popup popup;
        public MyMessageBox()
        {
            var rooFrame = App.RootFrame;
            var page = FindChildOfType<PhoneApplicationPage>(rooFrame);
            page.BackKeyPress += page_BackKeyPress;

        }

        void page_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (popup.IsOpen)
            {
                e.Cancel = true;
                popup.IsOpen = false;
            }
        }

        public T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typeChild = child as T;
                    if (typeChild != null)
                    {
                        return typeChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        public void Show()
        {
            popup = new Popup();
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(new WindowsPhoneControl1());
            stackPanel.Children.Add(new Rectangle { Height = 800, Width = 480, Fill = new SolidColorBrush(Colors.Gray), Opacity = 0.5 });
            popup.Child = stackPanel;
            popup.IsOpen = true;
        }
    }
}
