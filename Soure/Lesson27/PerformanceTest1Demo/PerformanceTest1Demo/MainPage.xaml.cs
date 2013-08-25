using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PerformanceTest1Demo.Resources;
using System.Threading.Tasks;
using System.Threading;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using Microsoft.Phone.Info;

namespace PerformanceTest1Demo
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            storyboard.Begin();
            Testing();
            //SaveSomething();
        }

        List<byte[]> listbytes = new List<byte[]>();
        private void Testing()
        {
            for(int i=0;i<20;i++)
            {
                Task.Factory.StartNew(() =>
                    {
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        //Thread.Sleep(500);
                        SaveSomething();
                        listbytes.Add(new byte[1024*8]);
                        Debug.WriteLine(DeviceStatus.ApplicationCurrentMemoryUsage / (1024) + "k");

                        watch.Stop();
                        var useTime = (double)watch.ElapsedMilliseconds;

                        Debug.WriteLine("方法Testing 流程"+i+"使用的时间" + useTime + "毫秒");
                    });
            }
        }

        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveSomething();
        }

        private void SaveSomething()
        {
            Task.Factory.StartNew(() =>
                   {
                       IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();
                       //for (int i = 0; i < 20; i++)
                       //{
                       IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(Guid.NewGuid().ToString() + ".dat", System.IO.FileMode.Create, isoStore);
                       isoStream.Write(new byte[1024 * 1024 * 20], 0, 1024 * 1024 * 20);
                       isoStream.Close();
                       Debug.WriteLine("正在写入文件");
                       // }
                       // long availableSpace = isoStore.AvailableFreeSpace;
                       // long quota = isoStore.Quota;
                       isoStore.Dispose();
                   });
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