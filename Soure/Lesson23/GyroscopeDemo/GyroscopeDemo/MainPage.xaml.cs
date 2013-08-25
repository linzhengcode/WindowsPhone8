using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GyroscopeDemo.Resources;

using Windows.Devices.Sensors;
using Microsoft.Xna.Framework;

namespace GyroscopeDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        Gyrometer gyrometer;
        GyrometerReading gyrometerReading;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
             
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                gyrometer = Gyrometer.GetDefault();
                if (gyrometer == null)
                {
                    MessageBox.Show("不支持陀螺仪");
                    return;
                }

                gyrometer.ReportInterval = 1000;
                gyrometer.ReadingChanged += gyrometer_ReadingChanged;
                gyrometerReading = gyrometer.GetCurrentReading();
                ShowData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        void gyrometer_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    gyrometerReading = args.Reading;
                    ShowData();
                });
        }

        void ShowData()
        {
            statusTextBlock.Text = "正在接受数据";

            currentXTextBlock.Text = gyrometerReading.AngularVelocityX.ToString("0.000");
            currentYTextBlock.Text = gyrometerReading.AngularVelocityY.ToString("0.000");
            currentZTextBlock.Text = gyrometerReading.AngularVelocityZ.ToString("0.000");

            cumulativeXTextBlock.Text = MathHelper.ToDegrees((float)gyrometerReading.AngularVelocityX).ToString("0.00");
            cumulativeYTextBlock.Text = MathHelper.ToDegrees((float)gyrometerReading.AngularVelocityY).ToString("0.00");
            cumulativeZTextBlock.Text = MathHelper.ToDegrees((float)gyrometerReading.AngularVelocityZ).ToString("0.00");

            double centerX = cumulativeGrid.ActualWidth / 2.0;
            double centerY = cumulativeGrid.ActualHeight / 2.0;

            currentXLine.X2 = centerX + gyrometerReading.AngularVelocityX * 100;
            currentYLine.X2 = centerX + gyrometerReading.AngularVelocityY * 100;
            currentZLine.X2 = centerX + gyrometerReading.AngularVelocityZ * 100;

            cumulativeXLine.X2 = centerX - centerY * Math.Sin(gyrometerReading.AngularVelocityX);
            cumulativeXLine.Y2 = centerY - centerY * Math.Cos(gyrometerReading.AngularVelocityX);
            cumulativeYLine.X2 = centerX - centerY * Math.Sin(gyrometerReading.AngularVelocityY);
            cumulativeYLine.Y2 = centerY - centerY * Math.Cos(gyrometerReading.AngularVelocityY);
            cumulativeZLine.X2 = centerX - centerY * Math.Sin(gyrometerReading.AngularVelocityZ);
            cumulativeZLine.Y2 = centerY - centerY * Math.Cos(gyrometerReading.AngularVelocityZ);
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