using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DelayLoadDemo.Resources;

namespace DelayLoadDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        //http://pic002.cnblogs.com/images/2012/152755/2012120917494440.png
        public MainPage()
        {
            InitializeComponent();

            List<Data> Items = new List<Data>();
            for (int i = 0; i < 1000; i++)
            {
                Items.Add(new Data{ Name="Test"+i, ImageUri= new Uri("http://pic002.cnblogs.com/images/2012/152755/2012120917494440.png?index="+i)});
            }
            LongList.ItemsSource=Items;
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
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