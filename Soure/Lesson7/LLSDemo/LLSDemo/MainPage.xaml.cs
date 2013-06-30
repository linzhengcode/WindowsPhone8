using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LLSDemo.Resources;

namespace LLSDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            List<Item> mainItem = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                mainItem.Add(new Item { Content = "A类别", Title = "Test A" + i });
                mainItem.Add(new Item { Content = "B类别", Title = "Test B" + i });
                mainItem.Add(new Item { Content = "C类别", Title = "Test C" + i });
            }
            var Items = (from item in mainItem group item by item.Content into newItems select new ItemInGroup<Item>(newItems.Key, newItems.ToList())).ToList();
            LongList.ItemsSource = Items;
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        public class ItemInGroup<T> : List<T>
        {
            public string Key { get; set; }

            public ItemInGroup(string key, List<T> items)
            {
                Key = key;
                this.AddRange(items);
            }
        }

        class Item
        {
            public string Title { get; set; }
            public string Content { get; set; }
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