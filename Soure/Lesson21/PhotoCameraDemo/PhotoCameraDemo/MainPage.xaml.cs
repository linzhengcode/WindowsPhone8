using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhotoCameraDemo.Resources;
using Microsoft.Devices;
using Microsoft.Xna.Framework.Media;

namespace PhotoCameraDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoCamera cam;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((PhotoCamera.IsCameraTypeSupported(CameraType.Primary) == true) ||
             (PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) == true))
            {
                if (PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing))
                {
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);
                }
                else
                {
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
                }

                viewfinderBrush.SetSource(cam);
                cam.Initialized += cam_Initialized;
                cam.CaptureCompleted += cam_CaptureCompleted;
                cam.CaptureImageAvailable += cam_CaptureImageAvailable;
                cam.AutoFocusCompleted += cam_AutoFocusCompleted;

                // 当按下快门按钮并保持大约 800 毫秒时。短于该时间的半按压将不会触发该事件。
                CameraButtons.ShutterKeyHalfPressed += OnButtonHalfPress;
                // 当快门按钮收到一个完全按压时。
                CameraButtons.ShutterKeyPressed += OnButtonFullPress;
                // 当松开快门按钮时。
                CameraButtons.ShutterKeyReleased += OnButtonRelease;
            }

            base.OnNavigatedTo(e);
        }

        // 自动对焦处理
        private void OnButtonHalfPress(object sender, EventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    cam.Focus();
                }
                catch (Exception focusError)
                {
                }
            }
        }

        // 拍照
        private void OnButtonFullPress(object sender, EventArgs e)
        {
            if (cam != null)
            {
                cam.CaptureImage();
            }
        }

        // 取消对焦
        private void OnButtonRelease(object sender, EventArgs e)
        {

            if (cam != null)
            {
                cam.CancelFocus();
            }
        }

        void cam_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(() => msg.Text = "对焦完成");
        }

        void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            MediaLibrary library = new MediaLibrary();
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            library.SavePictureToCameraRoll(fileName, e.ImageStream);
            Dispatcher.BeginInvoke(() => msg.Text = "照片保存成功");
        }

        void cam_CaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                Dispatcher.BeginInvoke(() => msg.Text = "拍照完成");
            }
            else
            {
                Dispatcher.BeginInvoke(() => msg.Text = "拍照失败");
            }
        }

        void cam_Initialized(object sender, CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                Dispatcher.BeginInvoke(() => msg.Text = "初始化成功");
            }
            else
            {
                Dispatcher.BeginInvoke(() => msg.Text = "初始化失败");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    cam.CaptureImage();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (cam.IsFlashModeSupported(FlashMode.On))
            {
                cam.FlashMode = FlashMode.On;
                msg.Text = "闪光灯打开";
            }
            else
            {
                msg.Text = "不支持闪光灯";
            }
        }

        private void viewfinderCanvas_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (cam != null)
            {
                if (cam.IsFocusAtPointSupported == true)
                {
                    try
                    {
                        Point tapLocation = e.GetPosition(viewfinderCanvas);
                        double focusXPercentage = tapLocation.X / viewfinderCanvas.Width;
                        double focusYPercentage = tapLocation.Y / viewfinderCanvas.Height;
                        cam.FocusAtPoint(focusXPercentage, focusYPercentage);
                    }
                    catch (Exception focusError)
                    {
                    }
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (cam != null)
            {
                var sizeList = cam.AvailableResolutions;

                cam.Resolution = sizeList.ToList()[0];
                msg.Text = "分辨率"+cam.Resolution.Height+" "+cam.Resolution.Width;
            }
        }

    }
}