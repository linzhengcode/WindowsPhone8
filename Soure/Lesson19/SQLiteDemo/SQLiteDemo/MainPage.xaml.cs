using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLiteDemo.Resources;
using Sqlite;
using Windows.Storage;
using SQLite;

namespace SQLiteDemo
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
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path+"\\people.db");
            await conn.CreateTableAsync<Person>();
            MessageBox.Show("创建成功");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\people.db");
            Person person = new Person
            {
                Name = "张三",
                Work = "程序员"
            };
            await conn.InsertAsync(person);
            Person person2 = new Person
            {
                Name = "李四",
                Work = "设计师"
            };
            await conn.InsertAsync(person2);
            MessageBox.Show("插入数据成功");
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\people.db");
            var query = conn.Table<Person>();//.Where(x => x.Name == "张三");
            var result = await query.ToListAsync();
            listbox.ItemsSource = result;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\people.db");
            var query = conn.Table<Person>().Where(x => x.Name == "张三");
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                await conn.DeleteAsync(item);
            }
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(ApplicationData.Current.LocalFolder.Path + "\\people.db");
            var query = conn.Table<Person>().Where(x => x.Name == "张三");
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                item.Work = "产品经理";
                await conn.UpdateAsync(item);
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