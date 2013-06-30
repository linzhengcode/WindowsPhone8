using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NFCSimpleDemo.Resources;
using Windows.Networking.Proximity;

namespace NFCSimpleDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        ProximityDevice _device;
        long Id;
        long Id2;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            _device = ProximityDevice.GetDefault();
            if (_device != null)
            {
                _device.DeviceArrived += _device_DeviceArrived;
                _device.DeviceDeparted += _device_DeviceDeparted;
            }
            else
            {
                MessageBox.Show("你的设备不支持NFC功能");
            }
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        void _device_DeviceDeparted(ProximityDevice sender)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            state.Text = "当前不处于NFC通信的范围内");
        }

        void _device_DeviceArrived(ProximityDevice sender)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            state.Text = "当前处于NFC通信的范围内");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_device != null)
            {
                Id = _device.PublishMessage("Windows.SampleMessageType", "Hello World!");
               // _device.StopPublishingMessage(Id);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (_device != null)
            {
                Id2 = _device.SubscribeForMessage("Windows.SampleMessageType", messageReceived);
            }
        }

        private void messageReceived(ProximityDevice sender, ProximityMessage message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
           MessageBox.Show(message.DataAsString));
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