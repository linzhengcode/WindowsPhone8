using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WebClientDemo.Resources;
using System.Diagnostics;
using Microsoft.Phone.Tasks;
using System.IO;

namespace WebClientDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        string url = "http://dldir1.qq.com/qqfile/qq/QQ2013/2013Beta5/6970/QQ2013Beta5.exe";
        Stopwatch stopwatch;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stopwatch = Stopwatch.StartNew();
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
           // webClient.OpenReadCompleted += webClient_OpenReadCompleted;
            webClient.DownloadStringAsync(new Uri(url));
        }

        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            //e.Result
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //e.Result
            MessageBox.Show("下载完成");
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            if (stopwatch.Elapsed.TotalSeconds != 0)
            {
                speed.Text = e.BytesReceived / (1024 * stopwatch.Elapsed.TotalSeconds) + "KB/s";
            }
            stopwatch.Start();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK)
            {
                return;
            }

            WebClient webClient = new WebClient();
            webClient.OpenWriteCompleted += (s, args) =>
                {
                    using (BinaryReader br = new BinaryReader(e.ChosenPhoto))
                    {
                        using (BinaryWriter bw = new BinaryWriter(args.Result))
                        {
                            long bCount = 0;
                            long fileSize = e.ChosenPhoto.Length;
                            byte[] bytes = new byte[2 * 1024];
                            do
                            {
                                bytes = br.ReadBytes(2 * 1024);
                                bCount += bytes.Length;
                                bw.Write(bytes);

                            } while (bCount < fileSize);
                        }
                    }
                };
            webClient.WriteStreamClosed += webClient_WriteStreamClosed;
            webClient.OpenWriteAsync(new Uri("http://www.cnblogs.com"));
        }

        void webClient_WriteStreamClosed(object sender, WriteStreamClosedEventArgs e)
        {
            MessageBox.Show("上传完成");
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