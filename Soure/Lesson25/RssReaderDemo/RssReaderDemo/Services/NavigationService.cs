using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RssReaderDemo.Services
{
    public class NavigationService : INavigationService
    {
        private PhoneApplicationFrame _mainFrame;

        public void GoBack()
        {
            if (EnsureMainFrame()
                && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }

        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                _mainFrame.Navigate(pageUri);
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                return true;
            }

            return false;
        }
    }
}
