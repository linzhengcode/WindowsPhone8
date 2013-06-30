using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LockScreenDemo.Resources;
using Windows.Phone.System.UserProfile;
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;

namespace LockScreenDemo
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var tile = ShellTile.ActiveTiles.First();
            var data = new FlipTileData
            {
                Count = 1,
                Title = "title",
                BackContent = "BackContent"
            };
            tile.Update(data);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var tile = ShellTile.ActiveTiles.First();
            var data = new FlipTileData
            {
                Count = 0,
                Title = "title",
                BackContent = "BackContent"
            };
            tile.Update(data);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
             await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (await LockScreenManager.RequestAccessAsync() == LockScreenRequestResult.Granted)
            {
                var uri = new Uri("ms-appx:///Assets/ls.png", UriKind.Absolute);
                LockScreen.SetImageUri(uri);
            }
            else
            {
                MessageBox.Show("你决绝了锁屏设置");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            string path = Guid.NewGuid().ToString() + ".png";
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream iof = iso.CreateFile(path);
            e.ChosenPhoto.CopyTo(iof, (int)e.ChosenPhoto.Length);
            e.ChosenPhoto.Close();
            iof.Close();
            var uri = new Uri("ms-appdata:///Local/" + path, UriKind.Absolute);
            LockScreen.SetImageUri(uri);
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