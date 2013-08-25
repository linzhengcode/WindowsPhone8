using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TaskFeaturesDemo.Resources;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TaskFeaturesDemo
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
            //Continu();
            //DelayTest();
            CancelTest();
            //Task task = new Task(() =>
            //    {
            //        Doing();
            //    });

            //task.Start();
        }

        private void CancelTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;
            Task task1 = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        Debug.WriteLine("任务1在执行中");
                        if (token.IsCancellationRequested)
                        {
                            Debug.WriteLine("任务被取消了");
                            break;
                        }

                    }
                }, token);
            Thread.Sleep(2000);
            cts.Cancel();
        }

        

        private async void DelayTest()
        {
            Task delayTime = Task.Delay(5000);
            await delayTime;

            //

        }

        private void Continu()
        {
            Task task1 = new Task(() =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine("任务1运行的线程" + Thread.CurrentThread.ManagedThreadId);
                });

            Task task2 = task1.ContinueWith((t) =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine("任务2运行的线程" + Thread.CurrentThread.ManagedThreadId);

                });

            Task task3 = task2.ContinueWith((t) =>
            {
                Thread.Sleep(500);
                Debug.WriteLine("任务3运行的线程" + Thread.CurrentThread.ManagedThreadId);

            });

            task1.Start();
        }

        private void Doing()
        {
            Task task1 = new Task(() =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine("任务1运行的线程" + Thread.CurrentThread.ManagedThreadId);
                });
            Task task2 = new Task(() =>
            {
                Thread.Sleep(500);
                Debug.WriteLine("任务2运行的线程" + Thread.CurrentThread.ManagedThreadId);
            });
            Task task3 = new Task(() =>
            {
                Thread.Sleep(500);
                Debug.WriteLine("任务3运行的线程" + Thread.CurrentThread.ManagedThreadId);
            });
            Task task4 = new Task(() =>
            {
                Thread.Sleep(500);
                Debug.WriteLine("任务4运行的线程" + Thread.CurrentThread.ManagedThreadId);
            });

            Debug.WriteLine("任务0运行的线程" + Thread.CurrentThread.ManagedThreadId);
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            Task.WaitAll(new Task[] { task1, task2, task3, task4 });

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