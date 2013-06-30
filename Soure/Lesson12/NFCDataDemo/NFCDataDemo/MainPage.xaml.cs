using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NFCDataDemo.Resources;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;

namespace NFCDataDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        ProximityDevice _device;
        StreamSocket _streamSocket;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            _device = ProximityDevice.GetDefault();
            if (_device != null)
            {
                PeerFinder.TriggeredConnectionStateChanged += PeerFinder_TriggeredConnectionStateChanged;
            }
            else
            {
                MessageBox.Show("你的设备不支持NFC功能");
            }
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        void PeerFinder_TriggeredConnectionStateChanged(object sender, TriggeredConnectionStateChangedEventArgs args)
        {
            switch (args.State)
            {
                case TriggeredConnectState.Listening:
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
            state.Text = "Listening");
                    // 作为主机正在监听等待连接
                    break;
                case TriggeredConnectState.PeerFound:
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
           state.Text = "PeerFound");
                    // 触碰完成
                    break;
                case TriggeredConnectState.Connecting:
                    // 正在连接
                    break;
                case TriggeredConnectState.Completed:
                    // 连接完成返回 StreamSocket对象可用于进行收发消息
                    _streamSocket = args.Socket;
                    break;
                case TriggeredConnectState.Canceled:
                    //在完成之前，连接已停止
                    break;
                case TriggeredConnectState.Failed:
                    // 连接失败
                    break;
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