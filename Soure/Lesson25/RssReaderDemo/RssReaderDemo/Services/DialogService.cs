using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RssReaderDemo.Services
{
    public class DialogService : IDialogService
    {
        public void ShowError(string errorMessage, string title, string buttonText)
        {
            MessageBox.Show(errorMessage, title, MessageBoxButton.OK);
        }

        public void ShowError(Exception error, string title, string buttonText)
        {
            MessageBox.Show(error.Message, title, MessageBoxButton.OK);
        }

        public void ShowMessage(string message, string title, string buttonText)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
    }
}
