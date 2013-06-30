using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DatagramSocketDemo.Resources;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Networking;

namespace DatagramSocketDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        int i = 0;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private async void listener_Click(object sender, RoutedEventArgs e)
        {
            DatagramSocket datagramSocket = new DatagramSocket();
            datagramSocket.MessageReceived += datagramSocket_MessageReceived;
            try
            {
                await datagramSocket.BindServiceNameAsync("22112");
                msgList.Children.Add(new TextBlock { Text = "监听成功" });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        async void datagramSocket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            i++;
            DataReader dataReader = args.GetDataReader();
            uint length = dataReader.UnconsumedBufferLength;
            string content = dataReader.ReadString(length);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            msgList.Children.Add(new TextBlock { Text = "服务器收到的消息：" + content }));

            //HostName hostName = new HostName("localhost");
            //DatagramSocket datagramSocket = new DatagramSocket();
            //IOutputStream outputStream = await datagramSocket.GetOutputStreamAsync(hostName, "22112");
            DataWriter writer = new DataWriter(sender.OutputStream);
            writer.WriteString(content + "|" + i);
            try
            {
                await writer.StoreAsync();
                msgList.Children.Add(new TextBlock { Text = "服务器发送的消息：" + content + "|" + i });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        private async void received_Click(object sender, RoutedEventArgs e)
        {
            DatagramSocket datagramSocket = new DatagramSocket();
            datagramSocket.MessageReceived+=datagramSocket_MessageReceived2;
            await datagramSocket.ConnectAsync(new HostName("localhost"), "22112");
        }

        void datagramSocket_MessageReceived2(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            i++;
            DataReader dataReader = args.GetDataReader();
            uint length = dataReader.UnconsumedBufferLength;
            string content = dataReader.ReadString(length);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
           msgList.Children.Add(new TextBlock { Text = "客户端收到的消息：" + content }));
        }

        private async void send_Click(object sender, RoutedEventArgs e)
        {
            HostName hostName = new HostName("localhost");
            DatagramSocket datagramSocket = new DatagramSocket();
            IOutputStream outputStream = await datagramSocket.GetOutputStreamAsync(hostName, "22112");
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteString("test"+i);
            try
            {
                await writer.StoreAsync();
                msgList.Children.Add(new TextBlock { Text = "客户端发送的消息：" + "test" + i });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
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