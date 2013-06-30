using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FileAssociationTestDemo.Resources;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;

namespace FileAssociationTestDemo
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

        private async void OnCreateFileButtonClicked(object sender, RoutedEventArgs e)
        {

            await WriteToFile("11111111", "rss.log");

        }


        public async Task WriteToFile(string contents, string fileName, StorageFolder folder = null)
        {
            folder = folder ?? ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(
                fileName,
                CreationCollisionOption.ReplaceExisting);
            using (var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var outStream = fs.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new DataWriter(outStream))
                    {
                        if (contents != null)
                            dataWriter.WriteString(contents);

                        await dataWriter.StoreAsync();
                        dataWriter.DetachStream();
                    }

                    await outStream.FlushAsync();
                }
            }
        }


        private async void OnOpenFileButtonClicked(object sender, RoutedEventArgs e)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("rss.log");
            Windows.System.Launcher.LaunchFileAsync(file);
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