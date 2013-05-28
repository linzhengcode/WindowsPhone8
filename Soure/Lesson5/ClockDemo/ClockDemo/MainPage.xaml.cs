using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ClockDemo.Resources;

namespace ClockDemo
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

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;
            //计算角度
            //时
            double hourAngle = ((float)now.Hour) / 12 * 360 + now.Minute / 2;
            //分
            double minuteAngle = ((float)now.Minute) / 60 * 360 + now.Second / 10;
            //秒
            double secondAngle = ((float)now.Second) / 60 * 360;
            HourAnimation.From = hourAngle;
            HourAnimation.To = hourAngle + 360;
            MinuteAnimation.From = minuteAngle;
            MinuteAnimation.To = minuteAngle + 360;
            SecondAnimation.From = secondAngle;
            SecondAnimation.To = secondAngle + 360;
            ClockStoryboard.Begin();//启动与此 Storyboard 关联的那组动画
           // ClockStoryboard.Begin();
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