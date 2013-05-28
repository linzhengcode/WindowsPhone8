using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AnimationDemo.Resources;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;

namespace AnimationDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        DispatcherTimer timer;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void myButton_Click_1(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animaton;
            animaton = new DoubleAnimation();
            animaton.From = 100;
            animaton.To = 500;
            animaton.Duration = new Duration(TimeSpan.FromMilliseconds(6000));
            Storyboard.SetTarget(animaton, myButton);
            Storyboard.SetTargetProperty(animaton, new PropertyPath("(UIElement.Height)"));
            storyboard.Children.Add(animaton);
            storyboard.Begin();
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromMilliseconds(100);
            //timer.Tick += timer_Tick;
            //timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (myButton.Height > 500)
            {
                timer.Stop();
                return;
            }
            myButton.Height += 5;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(2000);
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