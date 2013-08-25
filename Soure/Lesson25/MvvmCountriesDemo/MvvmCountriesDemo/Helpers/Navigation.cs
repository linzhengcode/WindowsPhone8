using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MvvmCountriesDemo.Helpers
{
    public static class Navigation
    {
        public static int Id { get; set; }

        public static PhoneApplicationPage Page
        {
            get
            {
                PhoneApplicationPage page = FindChildOfType<PhoneApplicationPage>(Application.Current.RootVisual);
                return page;
            }
        }

        public static T FindChildOfType<T>(DependencyObject root) where T : class
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

        public static void NavigationTo(string uri)
        {
            Page.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        public static void GotoCountryDetails()
        {
            Page.NavigationService.Navigate(new Uri("/CountryDetails.xaml", UriKind.Relative));
        }
    }
}
