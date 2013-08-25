using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FunctionClock.Resources;
using FunctionClock.Commons;
using FunctionClock.Services;

namespace FunctionClock
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            BuildLocalizedApplicationBar();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            weatherControl.RefreshWeatherData();
            base.OnNavigatedTo(e);
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Minimized;
            // 使用 AppResources 中的本地化字符串创建新菜单项。
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("闹铃列表");
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);

            ApplicationBarMenuItem appBarMenuItem2 = new ApplicationBarMenuItem("设置");
            appBarMenuItem2.Click += appBarMenuItem2_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem2);
        }

        void appBarMenuItem2_Click(object sender, EventArgs e)
        {
            NavigationHelper.NavigateTo(Uris.SettingsUri);
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationHelper.NavigateTo(Uris.AlarmsUri);
        }
    }
}