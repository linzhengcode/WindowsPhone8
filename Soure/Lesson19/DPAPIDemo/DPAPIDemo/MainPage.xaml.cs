using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DPAPIDemo.Resources;
using System.Text;
using System.Security.Cryptography;
using System.IO.IsolatedStorage;
using System.IO;

namespace DPAPIDemo
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
            byte[] PinByte = Encoding.UTF8.GetBytes(tb.Text);
            byte[] ProtectedPinByte = ProtectedData.Protect(PinByte, null);
            this.WritePinToFile(ProtectedPinByte);
        }

        private void WritePinToFile(byte[] pinData)
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream writestream = new IsolatedStorageFileStream("test.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write, file);
            Stream writer = new StreamWriter(writestream).BaseStream;
            writer.Write(pinData, 0, pinData.Length);
            writer.Close();
            writestream.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            byte[] ProtectedPinByte = this.ReadPinFromFile();
            byte[] PinByte = ProtectedData.Unprotect(ProtectedPinByte, null);
            string txt = Encoding.UTF8.GetString(PinByte, 0, PinByte.Length);
            MessageBox.Show(txt);
        }

        private byte[] ReadPinFromFile()
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream readstream = new IsolatedStorageFileStream("test.txt", System.IO.FileMode.Open, FileAccess.Read, file);
            Stream reader = new StreamReader(readstream).BaseStream;
            byte[] pinArray = new byte[reader.Length];
            reader.Read(pinArray, 0, pinArray.Length);
            reader.Close();
            readstream.Close();
            return pinArray;
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