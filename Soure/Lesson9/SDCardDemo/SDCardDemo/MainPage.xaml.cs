using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SDCardDemo.Resources;
using Microsoft.Phone.Storage;

namespace SDCardDemo
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
            var externalStorageDevices = await ExternalStorage.GetExternalStorageDevicesAsync();
            if (externalStorageDevices.Count() == 0)
            {
                MessageBox.Show("没有sd卡");
                return;
            }
            ExternalStorageDevice externalStorageDevice = externalStorageDevices.First();
            if (externalStorageDevice != null)
            {
                var folder = externalStorageDevice.RootFolder;
                if (folder != null)
                {
                    var files = await folder.GetFilesAsync();
                    MessageBox.Show(files.Count().ToString());
                }
            }
            else
            {
                MessageBox.Show("没有sd卡");
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