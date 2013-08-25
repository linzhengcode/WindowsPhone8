using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ParallelTaskDemo.Resources;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace ParallelTaskDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<TaskCompletionSource<string>> taskList;

        private AsyncLock asyncLock = new AsyncLock();

        private object o = new object();
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            taskList = new List<TaskCompletionSource<string>>();
            for (int i = 0; i < 10; i++)
            {
                taskList.Add(new TaskCompletionSource<string>());
            }
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew( async () =>
                {
                    Debug.WriteLine("任务1开始运行");
                    await Task.Delay(3000);
                    for (int i = 0; i < 10; i++)
                    {
                        await Task.Delay(1000);
                        //
                        taskList[i].SetResult("任务1 完成了相关的处理" + i);
                        Debug.WriteLine("通知任务2：任务1 完成了相关的处理" + i);
                    }

                });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(async  () =>
            {
                Debug.WriteLine("任务2开始运行");
                await Task.Delay(3000);
                foreach (var tcc in taskList)
                {
                    string result = await tcc.Task;
                    Debug.WriteLine("收到任务1的通知：" + result);
                    //

                }
            });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AsynTask();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Task.Factory.StartNew(async () =>
            //    {

            //        await Task.Delay(50);
            //        task();
            //    });
            //}
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AsynTask();
            //for (int i = 0; i < 1000; i++)
            //{

            //    Task.Factory.StartNew(async () =>
            //    {

            //        await Task.Delay(50);
            //        task();
            //    });
            //}
        }

        private void task()
        {
            try
            {
                lock (o)
                {
                    IsolatedStorageSettings setting = IsolatedStorageSettings.ApplicationSettings;
                    if (setting.Contains("keytest"))
                    {
                        setting["keytest"] = "test";
                    }
                    else
                    {
                        setting.Add("keytest", "test");
                    }
                    setting.Save();
                }
                //Debug.WriteLine("操作成功");
            }
            catch (Exception e)
            {
                Debug.WriteLine("操作失败");
                Debug.WriteLine(e.Message);
            }
        }

        private void AsynTask()
        {
            Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 20; i++)
                {
                    using (await asyncLock.LockAsync())
                    {
                        await MyTask(i);
                    }
                    await Task.Delay(50);
                }
            });
        }
        string str;
        private async Task MyTask(int i)
        {
            str = "当前的string" + i;
            Debug.WriteLine(str);
            await Task.Delay(1000);
            str = "修改的"+i;
            Debug.WriteLine(str);
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