using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FunctionClock.Commons
{
    public class NavigationHelper
    {
        public static object Parameter;
        private static PhoneApplicationFrame _frame;

        private static bool CanUsePhoneApplicationFrame()
        {
            if (_frame != null)
            {
                return true;
            }
            _frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (_frame != null)
            {
                return true;
            }
            return false;
        }

        public static void GoBack()
        {
            if (CanUsePhoneApplicationFrame() && _frame.CanGoBack)
            {
                Parameter = null;
                _frame.GoBack();
            }
        }

        public static void GoBack(object para)
        {
            if (CanUsePhoneApplicationFrame() && _frame.CanGoBack)
            {
                Parameter = para;
                _frame.GoBack();
            }
        }

        public static void NavigateTo(Uri uri)
        {
            if (CanUsePhoneApplicationFrame())
            {
                Parameter = null;
                _frame.Navigate(uri);
            }
        }

        public static void NavigateTo(Uri uri,object para)
        {
            if (CanUsePhoneApplicationFrame())
            {
                Parameter = para;
                _frame.Navigate(uri);
            }
        }
    }
}
