using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CompassDemo.Resources;

using Windows.Devices.Sensors;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CompassDemo
{
    public partial class MainPage : PhoneApplicationPage
    {

        Compass compass;
        CompassReading compassReading;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            compass = Compass.GetDefault();
            if (compass == null)
            {
                MessageBox.Show("不支持罗盘传感器");
                return;
            }
            compass.ReportInterval = 1000;
            compass.ReadingChanged += compass_ReadingChanged;
            compassReading = compass.GetCurrentReading();
            ShowData();
        }

        void compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    compassReading = args.Reading;
                    ShowData();
                });
        }

        void ShowData()
        {
            statusTextBlock.Text = "正在接收罗盘的数据  当前时间" + compassReading.Timestamp.DateTime.ToLongTimeString();
            magneticTextBlock.Text =  compassReading.HeadingMagneticNorth.ToString();
            trueTextBlock.Text = compassReading.HeadingTrueNorth.ToString();

            double centerX = headingGrid.ActualWidth / 2.0;
            double centerY = headingGrid.ActualHeight / 2.0;
            magneticLine.X2 = centerX - centerY * Math.Sin(MathHelper.ToRadians((float)compassReading.HeadingMagneticNorth));
            magneticLine.Y2 = centerY - centerY * Math.Cos(MathHelper.ToRadians((float)compassReading.HeadingMagneticNorth));
            trueLine.X2 = centerX - centerY * Math.Sin(MathHelper.ToRadians((float)compassReading.HeadingTrueNorth));
            trueLine.Y2 = centerY - centerY * Math.Cos(MathHelper.ToRadians((float)compassReading.HeadingTrueNorth));
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