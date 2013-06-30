using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BluetoothApp2AppDemo.Resources;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;

namespace BluetoothApp2AppDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        StreamSocket socket;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;
            base.OnNavigatedTo(e);
        }

        void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                  var result =  MessageBox.Show("是否接收" + args.PeerInformation.DisplayName + "连接请求", "蓝牙连接",
                         MessageBoxButton.OKCancel);

                  if (result== MessageBoxResult.OK)
                  {


                  }
                });
        }

        private async void Connect(PeerInformation peerInformation)
        {
            try
            {
                socket = await PeerFinder.ConnectAsync(peerInformation);
                PeerFinder.Stop();
                //同Socket TCP的编程方式来进行发送消息

            }
            catch (Exception eer)
            {
                MessageBox.Show(eer.Message);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PeerFinder.Start();
                var peers = await PeerFinder.FindAllPeersAsync();
                if (peers.Count() == 0)
                {
                    MessageBox.Show("没有找到其他应用");
                }
                else
                {
                    MessageBox.Show("等待对方的连接");
                    Connect(peers[0]);

                }

            }
            catch (Exception eer)
            {
                MessageBox.Show(eer.Message);
            }
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