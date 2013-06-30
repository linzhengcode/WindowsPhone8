using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BluetoothApp2DeviceDemo.Resources;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;

namespace BluetoothApp2DeviceDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        StreamSocket streamSocket;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var pairedDevices = await PeerFinder.FindAllPeersAsync();
                if (pairedDevices.Count == 0)
                {
                    MessageBox.Show("没有找到相关的蓝牙设备");
                }
                else
                {
                    streamSocket = new StreamSocket();
                    await streamSocket.ConnectAsync(pairedDevices[0].HostName, "1");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PeerFinder.AlternateIdentities["Bluetooth:SDP"] = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
            var pairedDevices = await PeerFinder.FindAllPeersAsync();
            if (pairedDevices.Count == 0)
            {
                MessageBox.Show("没有找到相关的蓝牙设备");
            }
            else
            {
                streamSocket = new StreamSocket();
                await streamSocket.ConnectAsync(pairedDevices[0].HostName, pairedDevices[0].ServiceName);
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