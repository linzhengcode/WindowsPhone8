using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DebugDemo.Resources;
using WindowsPhoneRuntimeComponent1;
using System.Diagnostics;

namespace DebugDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        int i = 3;
        int j = 7;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            WindowsPhoneRuntimeComponent rc = new WindowsPhoneRuntimeComponent();
            Test();
#if WINDOWS_PHONE
            i = 7;
            Debug.WriteLine("i的值" + i);
#endif

#if MY_WINDOWS_PHONE
            i = 9;
            Debug.WriteLine("i的值" + i);
#endif

#if DEBUG
            i = 9;
            Debug.WriteLine("i的值" + i);
#endif
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        public void Test()
        {

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