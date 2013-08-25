using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BackgroundTransferDemo.Resources;
using Microsoft.Phone.BackgroundTransfer;
using System.Windows.Resources;
using System.IO;
using System.IO.IsolatedStorage;

namespace BackgroundTransferDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            foreach (var transfer in BackgroundTransferService.Requests)
            {
                transfer.TransferStatusChanged +=transferRequest_TransferStatusChanged;
                transfer.TransferProgressChanged += transferRequest_TransferProgressChanged;
            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string mp3uri = "http://www.nmgwxc.com/m/yldcj.mp3";

            if (BackgroundTransferService.Requests.Count() >= 25)
            {
                MessageBox.Show("已经超过最大请求数量，请稍后");
                return;
            }
            if (BackgroundTransferService.Requests.Any(p => p.RequestUri.AbsoluteUri == mp3uri))
            {
                MessageBox.Show("文件已经在下载的请求中");
                return;
            }
            BackgroundTransferRequest transferRequest = new BackgroundTransferRequest(new Uri(mp3uri, UriKind.Absolute));
            transferRequest.Method = "GET";
            transferRequest.TransferPreferences = TransferPreferences.None;
            transferRequest.TransferStatusChanged += transferRequest_TransferStatusChanged;
            transferRequest.TransferProgressChanged += transferRequest_TransferProgressChanged;
            transferRequest.Tag = "yldcj.mp3";
            transferRequest.DownloadLocation = new Uri("shared/transfers/yldcj.mp3", UriKind.Relative);
            try
            {
                BackgroundTransferService.Add(transferRequest);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        void transferRequest_TransferProgressChanged(object sender, BackgroundTransferEventArgs e)
        {
            lls.ItemsSource = BackgroundTransferService.Requests.ToList();
        }

        void transferRequest_TransferStatusChanged(object sender, BackgroundTransferEventArgs e)
        {
            switch (e.Request.TransferStatus)
            {
                case TransferStatus.Completed:
                    if (e.Request.StatusCode == 200 || e.Request.StatusCode == 206)
                    {
                        MessageBox.Show("下载完成");

                    }
                    else
                    {
                        if (e.Request.TransferError != null)
                        {
                            MessageBox.Show(e.Request.TransferError.Message);
                        }
                    }
                    BackgroundTransferRequest temp = BackgroundTransferService.Find(e.Request.RequestId);
                    if (temp != null)
                    {
                        BackgroundTransferService.Remove(temp);
                    }
                    break;
                case TransferStatus.Waiting:

                    break;
                case TransferStatus.WaitingForWiFi:


                    break;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string uploadUri = "http://www.cnblogs.com"; 
            if (BackgroundTransferService.Requests.Count() >= 25)
            {
                MessageBox.Show("已经超过最大请求数量，请稍后");
                return;
            }
            if (BackgroundTransferService.Requests.Any(p => p.RequestUri.AbsoluteUri == uploadUri  +"?fileName=Making love without nothing at all.mp3"))
            {
                MessageBox.Show("文件已经在上传的请求中");
                return;
            }

            StreamResourceInfo sr = Application.GetResourceStream(new Uri("Assets/Making love without nothing at all.mp3", UriKind.Relative));
            using (BinaryReader br = new BinaryReader(sr.Stream))
            {
                byte[] data = br.ReadBytes((int)sr.Stream.Length);

                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                using (var isfs = new IsolatedStorageFileStream(@"shared/transfers/Making love without nothing at all.mp3", FileMode.Create, isf))
                {
                    using (var bw = new BinaryWriter(isfs))
                    {
                        bw.Write(data);
                    }
                }
            }

            BackgroundTransferRequest transferRequest = new BackgroundTransferRequest(new Uri(uploadUri + "?fileName=Making love without nothing at all.mp3", UriKind.Absolute));
            transferRequest.Method = "POST"; // 上传
            transferRequest.TransferPreferences = TransferPreferences.None;
            transferRequest.TransferStatusChanged += transferRequest_TransferStatusChanged;
            transferRequest.TransferProgressChanged += transferRequest_TransferProgressChanged;
            transferRequest.UploadLocation = new Uri("shared/transfers/Making love without nothing at all.mp3", UriKind.Relative);
            try
            {
                BackgroundTransferService.Add(transferRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AddBackgroundTransfer: " + ex.ToString());
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