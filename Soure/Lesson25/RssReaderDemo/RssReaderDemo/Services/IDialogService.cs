using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.Services
{
    public interface IDialogService
    {
        void ShowError(string errorMessage, string title, string buttonText);
        void ShowError(Exception error, string title, string buttonText);
        void ShowMessage(string message, string title, string buttonText);
    }
}
