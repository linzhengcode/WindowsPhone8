using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AccelerometerDemo.Resources;

using Windows.Devices.Sensors;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AccelerometerDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        Accelerometer accelerometer;
        AccelerometerReading accelerometerReading;

        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            accelerometer = Accelerometer.GetDefault();
            if (accelerometer == null)
            {
                MessageBox.Show("不支持加速度计传感器");
                return;
            }
            Debug.WriteLine(accelerometer.MinimumReportInterval);
            accelerometer.ReportInterval = accelerometer.MinimumReportInterval * 2;
            accelerometer.ReadingChanged += accelerometer_ReadingChanged;
            accelerometer.Shaken += accelerometer_Shaken;
            accelerometerReading = accelerometer.GetCurrentReading();
            ShowData();
        }

        void accelerometer_Shaken(Accelerometer sender, AccelerometerShakenEventArgs args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    statusTextBlock.Text = "手机在摇动 时间点"
                       + args.Timestamp.DateTime.ToLongTimeString();
                });
        }

        void accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                accelerometerReading = args.Reading;
                ShowData();
            });
        }

        void ShowData()
        {
            statusTextBlock.Text = "正在接收加速度数据 时间点"
                + accelerometerReading.Timestamp.DateTime.ToLongTimeString();
            // 显示数值
            xTextBlock.Text = "X: " + accelerometerReading.AccelerationX.ToString("0.00");
            yTextBlock.Text = "Y: " + accelerometerReading.AccelerationY.ToString("0.00");
            zTextBlock.Text = "Z: " + accelerometerReading.AccelerationZ.ToString("0.00");
            // 在图形上显示
            xLine.X2 = xLine.X1 + accelerometerReading.AccelerationX * 100;
            yLine.Y2 = yLine.Y1 - accelerometerReading.AccelerationY * 100;
            zLine.X2 = zLine.X1 - accelerometerReading.AccelerationZ * 50;
            zLine.Y2 = zLine.Y1 + accelerometerReading.AccelerationZ * 50;
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