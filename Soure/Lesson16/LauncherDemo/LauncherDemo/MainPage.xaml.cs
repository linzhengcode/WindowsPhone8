using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LauncherDemo.Resources;
using Windows.ApplicationModel;

namespace LauncherDemo
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

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("testdemo:[test]"));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IEnumerable<Package> apps = Windows.Phone.Management.Deployment.InstallationManager.FindPackagesForCurrentPublisher();
            if (apps.Count() != 0)
            {
                apps.First().Launch(string.Empty);
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //打开网页
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://www.baidu.com")); 

            //打开内置邮箱发送邮件 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("mailto:username@163.com")); 

            //呼叫手机 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("tel:10000000000")); 

            //启动Wi-Fi 设置 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:")); 
            //ms-settings-accounts:        启动帐户设置应用。 
            //ms-settings-airplanemode:    启动飞行模式设置应用。 
            //ms-settings-bluetooth:       启动蓝牙设置应用。 
            //ms-settings-cellular:        启动手机网络设置应用。 
            //ms-settings-emailandaccounts: 启动电子邮件和帐户设置应用。 
            //ms-settings-location:        启动位置设置应用。 
            //ms-settings-lock:            启动锁屏设置应用。 
            //ms-settings-wifi:            启动 Wi-Fi 设置应用。 

            //启动 Windows Phone 商店 并显示特定应用的详细信息页面。 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("zune:navigate?appid=fdf05477-814e-41d4-86cd-25d5a50ab2d8"));   
            //启动 商店 并显示调用应用的查看页面。 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("zune:reviewapp")); 
            //启动 商店 并显示特定应用的查看页面 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("zune:reviewapp?appid=appfdf05477-814e-41d4-86cd-25d5a50ab2d8")); 
            //启动 商店 并搜索 
            //Windows.System.Launcher.LaunchUriAsync(new Uri("zune:search?keyword=关键字&publisher=发布者名称&contenttype=app")); 
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