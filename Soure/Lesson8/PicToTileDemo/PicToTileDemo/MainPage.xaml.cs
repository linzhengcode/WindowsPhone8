using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PicToTileDemo.Resources;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media;

namespace PicToTileDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            time.Text = DateTime.Now.ToShortTimeString();
            WriteableBitmap bmp = new WriteableBitmap(tile, new CompositeTransform());
            string path ="Shared/ShellContent/"+ Guid.NewGuid().ToString() + ".jpg";
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream iof = iso.CreateFile(path);
            bmp.SaveJpeg(iof, 336, 336, 0, 100);
            iof.Close();
            ShellTile mainTile = ShellTile.ActiveTiles.First();
            var data = new StandardTileData
            {
                BackgroundImage = new Uri("isostore:/" + path, UriKind.Absolute)
            };
            mainTile.Update(data); 
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