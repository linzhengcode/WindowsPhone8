using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StreamSocketDemo.Resources;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Networking;

namespace StreamSocketDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        StreamSocket socket;
        DataWriter serverWriter;
        DataReader dataReader;
        int j = 0;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private async void listener_Click(object sender, RoutedEventArgs e)
        {
            StreamSocketListener streamSocketListener = new StreamSocketListener();
            streamSocketListener.ConnectionReceived += streamSocketListener_ConnectionReceived;
            try
            {
                await streamSocketListener.BindServiceNameAsync("22112");
                msgList.Children.Add(new TextBlock { Text = "监听成功" });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        async void streamSocketListener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            int i = 0;
            DataReader dataReader = new DataReader(args.Socket.InputStream);
            DataWriter serverWriter = new DataWriter(args.Socket.OutputStream);
            try
            {
                while (true)
                {
                    uint sizeCount = await dataReader.LoadAsync(4);
                    uint length = dataReader.ReadUInt32();
                    uint contentLength = await dataReader.LoadAsync(length);
                    string msg = dataReader.ReadString(contentLength);
                    i++;
                    Deployment.Current.Dispatcher.BeginInvoke(() => msgList.Children.Add(
                        new TextBlock { Text = "服务器接收到的消息：" + msg }));
                    string serverStr = msg + "|" + i;
                    serverWriter.WriteUInt32(serverWriter.MeasureString(serverStr));
                    serverWriter.WriteString(serverStr);
                    try
                    {
                        await serverWriter.StoreAsync();
                    }
                    catch (Exception err)
                    {
                        if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                        {

                        }
                    }
                }
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        private async void connect_Click(object sender, RoutedEventArgs e)
        {
            socket = new StreamSocket();
            HostName hostName = new HostName("localhost");
            try
            {
                await socket.ConnectAsync(hostName, "22112");
                msgList.Children.Add(new TextBlock { Text = "连接成功" });
                ReadMessage();
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        async void ReadMessage()
        {
            dataReader = new DataReader(socket.InputStream);
            while (true)
            {
                uint sizeCount = await dataReader.LoadAsync(4);
                uint length = dataReader.ReadUInt32();
                uint contentLength = await dataReader.LoadAsync(length);
                string msg = dataReader.ReadString(contentLength);
                msgList.Children.Add(
                     new TextBlock { Text = "客户端接收到的消息：" + msg });
            }
        }

        private async void send_Click(object sender, RoutedEventArgs e)
        {
            j++;
            serverWriter = new DataWriter(socket.OutputStream);
            string serverStr = "cilent test "+j ;
            serverWriter.WriteUInt32(serverWriter.MeasureString(serverStr));
            serverWriter.WriteString(serverStr);
            try
            {
                await serverWriter.StoreAsync();
                msgList.Children.Add(
                    new TextBlock { Text = "客户端发送的消息：" + serverStr });
            }
            catch (Exception err)
            {
                if (SocketError.GetStatus(err.HResult) == SocketErrorStatus.AddressAlreadyInUse)
                {

                }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (socket != null)
            {
                socket.Dispose();
                socket = null;
            }
            if (serverWriter != null)
            {
                serverWriter.DetachBuffer();
                serverWriter.Dispose();
                serverWriter = null;
            }
            if (dataReader != null)
            {
                dataReader.Dispose();
                dataReader = null;
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