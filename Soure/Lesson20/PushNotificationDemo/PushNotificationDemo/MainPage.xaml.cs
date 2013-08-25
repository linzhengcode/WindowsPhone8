using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PushNotificationDemo.Resources;
using Microsoft.Phone.Notification;
using System.Diagnostics;
using System.IO;

namespace PushNotificationDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private HttpNotificationChannel httpChannel;
        private const string channelName = "ChannelName";
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            httpChannel = HttpNotificationChannel.Find(channelName);
            //如果存在就删除
            if (httpChannel != null)
            {
                httpChannel.Close();
                httpChannel.Dispose();
            }
            httpChannel = new HttpNotificationChannel(channelName, "NotificationServer");
            httpChannel.ChannelUriUpdated += httpChannel_ChannelUriUpdated;
            httpChannel.ShellToastNotificationReceived += httpChannel_ShellToastNotificationReceived;
            httpChannel.HttpNotificationReceived += httpChannel_HttpNotificationReceived;
            httpChannel.ErrorOccurred += httpChannel_ErrorOccurred;
            httpChannel.Open();
            httpChannel.BindToShellToast();
            httpChannel.BindToShellTile();
        }

        void httpChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            if (e.ErrorType == ChannelErrorType.ChannelOpenFailed)
            {

            }
                 Dispatcher.BeginInvoke(() =>
           msgTextBlock.Text = e.Message);
            
        }

        void httpChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            using (var reader = new StreamReader(e.Notification.Body))
            {
                string msg = reader.ReadToEnd();
                Dispatcher.BeginInvoke(() =>
           msgTextBlock.Text = msg);
            }
        }

        void httpChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            string msg = string.Empty;
            foreach (var key in e.Collection.Keys)
            {
                msg += key + " : " + e.Collection[key] + Environment.NewLine;
            }
            Dispatcher.BeginInvoke(() =>
            msgTextBlock.Text = msg);
        }

        void httpChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Debug.WriteLine(e.ChannelUri);
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}