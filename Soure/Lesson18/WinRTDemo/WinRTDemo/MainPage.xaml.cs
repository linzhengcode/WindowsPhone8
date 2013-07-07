using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WinRTDemo.Resources;
using WindowsPhoneRuntimeComponent1;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace WinRTDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        WindowsPhoneRuntimeComponent windowsPhoneRuntimeComponent;

        Progress<double> myProgress;
        CancellationTokenSource cancellationTokenSource;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            windowsPhoneRuntimeComponent = new WindowsPhoneRuntimeComponent();
            windowsPhoneRuntimeComponent.currentValue += windowsPhoneRuntimeComponent_currentValue;
            myProgress = new Progress<double>();
            myProgress.ProgressChanged += myProgress_ProgressChanged;
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        void windowsPhoneRuntimeComponent_currentValue(int __param0)
        {
            Debug.WriteLine("事件汇报的进度：" + __param0.ToString());
        }

        void myProgress_ProgressChanged(object sender, double e)
        {
            Debug.WriteLine("当前处理进度："+e.ToString());
            if (e == 8)
            {
                cancellationTokenSource.Cancel();
            }
            //throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          int sum=  windowsPhoneRuntimeComponent.Add(1, 10);
          MessageBox.Show(sum.ToString());
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int sum = await windowsPhoneRuntimeComponent.AddAdync(1, 10);
            MessageBox.Show(sum.ToString());
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                //int sum = await windowsPhoneRuntimeComponent.AddWithProgressAsync(1, 10).AsTask(myProgress);
                  int sum = await windowsPhoneRuntimeComponent.AddWithProgressAsync(1, 10).AsTask(cancellationTokenSource.Token,myProgress);
                Debug.WriteLine("结果：" + sum);
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("任务被取消了");
            }
            catch (Exception)
            {

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