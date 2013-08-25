using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhotoCaptureDeviceDemo.Resources;
using Windows.Phone.Media.Capture;

using Microsoft.Devices;
using System.IO;
using Microsoft.Xna.Framework.Media;

namespace PhotoCaptureDeviceDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCaptureDevice captureDevice;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhotoCaptureDevice.AvailableSensorLocations.Contains(CameraSensorLocation.Back) ||
                PhotoCaptureDevice.AvailableSensorLocations.Contains(CameraSensorLocation.Front))
            {
                if (PhotoCaptureDevice.AvailableSensorLocations.Contains(CameraSensorLocation.Back))
                {
                    System.Collections.Generic.IReadOnlyList<Windows.Foundation.Size> SupportedResolutions =
                        PhotoCaptureDevice.GetAvailableCaptureResolutions(CameraSensorLocation.Back);
                    Windows.Foundation.Size res = SupportedResolutions[0];
                    captureDevice = await PhotoCaptureDevice.OpenAsync(CameraSensorLocation.Back, res);
                }
                else
                {
                    System.Collections.Generic.IReadOnlyList<Windows.Foundation.Size> SupportedResolutions =
                        PhotoCaptureDevice.GetAvailableCaptureResolutions(CameraSensorLocation.Front);
                    Windows.Foundation.Size res = SupportedResolutions[0];
                    captureDevice = await PhotoCaptureDevice.OpenAsync(CameraSensorLocation.Front, res);
                }
                viewfinderBrush.SetSource(captureDevice);
            }
            else
            {
                msg.Text = "相机不可用";
            }

            base.OnNavigatedTo(e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CameraCaptureSequence seq;
            seq = captureDevice.CreateCaptureSequence(1);

            seq.Frames[0].DesiredProperties[KnownCameraPhotoProperties.SceneMode] = CameraSceneMode.Portrait;

            MemoryStream captureStream1 = new MemoryStream();

            seq.Frames[0].CaptureStream = captureStream1.AsOutputStream();
            await captureDevice.PrepareCaptureSequenceAsync(seq);

            await seq.StartCaptureAsync();
            captureStream1.Seek(0, SeekOrigin.Begin);
            MediaLibrary mediaLibrary = new MediaLibrary();
            mediaLibrary.SavePictureToCameraRoll( "1111.jpg", captureStream1);
 
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