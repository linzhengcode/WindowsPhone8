using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;

namespace RssReaderDemo
{
    public partial class StatusControl : UserControl
    {

        public const string MessagePropertyName = "Message";
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            MessagePropertyName,
            typeof(string),
            typeof(StatusControl),
            new PropertyMetadata(string.Empty));
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public StatusControl()
        {
            InitializeComponent();
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }

        public async void Show(int timeout = 0)
        {
            Visibility = Visibility.Visible;

            if (timeout > 0)
            {
                await Task.Delay(timeout);
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
