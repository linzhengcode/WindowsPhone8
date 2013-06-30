using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FileDemo.Resources;
using Windows.Storage;
using System.Windows.Media.Imaging;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Windows.Storage.Streams;

namespace FileDemo
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
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync
                (new Uri("ms-appx:///Assets/ApplicationIcon.png"));
            if (file != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                Stream stream = await file.OpenStreamForReadAsync();
                bitmapImage.SetSource(stream);
                Image image = new Image();
                image.Source = bitmapImage;
                LayoutRoot.Children.Add(image);
                     
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var files = await storageFolder.GetFilesAsync();
            foreach (var file in files)
            {
                Debug.WriteLine(file.Name);
            }
            var folders = await storageFolder.GetFoldersAsync();
            foreach (var folder in folders)
            {
                Debug.WriteLine(folder.Name);
            }
            MessageBox.Show(files.Count().ToString());

        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            var files = await storageFolder.GetFilesAsync();
            foreach (var file in files)
            {
                Debug.WriteLine(file.Name);
            }
            var folders = await storageFolder.GetFoldersAsync();
            foreach (var folder in folders)
            {
                Debug.WriteLine(folder.Name);
            }
            MessageBox.Show(files.Count().ToString());
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
           await  WriteFile("text.txt", "hello Windows Phone 8");
        }

        public async Task WriteFile(string fileName, string text)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (Stream steam = await storageFile.OpenStreamForWriteAsync())
            {
                byte[] content = Encoding.UTF8.GetBytes(text);
                await steam.WriteAsync(content, 0, content.Length);
            }
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(await ReadFile("text.txt"));
        }

        public async Task<string> ReadFile(string fileName)
        {
            StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync
                (new Uri("ms-appdata:///local/" + fileName));
            IRandomAccessStream randomAccessStream = await storageFile.OpenReadAsync();
            Stream stream = randomAccessStream.AsStreamForRead();
            byte[] content = new byte[stream.Length];
            await stream.ReadAsync(content, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(content, 0, content.Length);
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