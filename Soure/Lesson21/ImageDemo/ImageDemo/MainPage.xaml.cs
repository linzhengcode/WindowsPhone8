using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ImageDemo.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ImageDemo
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
            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.PixelHeight = 400;
            photoChooserTask.PixelWidth = 200;
            photoChooserTask.Completed += photoChooserTask_Completed;
            photoChooserTask.Show();
        }

        void photoChooserTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(e.ChosenPhoto);
                image1.Source = bitmapImage;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(480, 800);
            writeableBitmap.Render(App.Current.RootVisual, null);
            writeableBitmap.Invalidate();
            MemoryStream memoryStream = new MemoryStream();
            writeableBitmap.SaveJpeg(memoryStream, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight,0, 100);
            memoryStream.Seek(0, SeekOrigin.Begin);
            MediaLibrary mediaLibrary = new MediaLibrary();
            mediaLibrary.SavePicture(Guid.NewGuid().ToString() + ".jpg", memoryStream);
            memoryStream.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RectangleGeometry rectangleGeometry = new RectangleGeometry();
            rectangleGeometry.Rect = new Rect(0, 0, 50, 50);
            image1.Clip = rectangleGeometry;

            WriteableBitmap writeableBitmaptemp = new WriteableBitmap(image1, null);

            Image image = new Image();
            image.Stretch = Stretch.None;
            image.Height = 50;
            image.Width = 50;
            image.Source = writeableBitmaptemp;

            WriteableBitmap writeableBitmap = new WriteableBitmap(image, null);
            MemoryStream memoryStream = new MemoryStream();
            writeableBitmap.SaveJpeg(memoryStream, writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, 0, 100);
            memoryStream.Seek(0, SeekOrigin.Begin);
            MediaLibrary mediaLibrary = new MediaLibrary();
            mediaLibrary.SavePicture(Guid.NewGuid().ToString() + ".jpg", memoryStream);
            memoryStream.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Image2();
        }

        private void Image1()
        {
            WriteableBitmap wb = new WriteableBitmap(image1, null);
            int[] ImageData = wb.Pixels;
            WriteableBitmap wb_gray = new WriteableBitmap(wb.PixelWidth, wb.PixelHeight);

            for (int i = 0; i < wb.PixelHeight; i++)
            {
                for (int j = 0; j < wb.PixelWidth; j++)
                {
                    int curColor = ImageData[i * wb.PixelWidth + j];

                    byte RedValue = (byte)(curColor >> 16 & 0xFF);
                    byte GreenValue = (byte)(curColor >> 8 & 0xFF);
                    byte BlueValue = (byte)(curColor & 0xFF);
                    //灰度处理的公式
                    byte GrayValue = (byte)(RedValue * 0.299 + GreenValue * 0.587 + BlueValue * 0.114);

                    byte[] GrayValueArr = new byte[4];
                    GrayValueArr[3] = 0xFF;
                    GrayValueArr[2] = GrayValue;
                    GrayValueArr[1] = GrayValue;
                    GrayValueArr[0] = GrayValue;

                    int GrayPixel = BitConverter.ToInt32(GrayValueArr, 0);

                    wb_gray.Pixels[i * wb.PixelWidth + j] = GrayPixel;

                }
            }

            wb_gray.Invalidate();
            image1.Source = wb_gray;

        }

        private void Image2()
        {
            WriteableBitmap oldbitmap = new WriteableBitmap(image1, null);
            int[] ImageData = oldbitmap.Pixels;
            WriteableBitmap newbitmap = new WriteableBitmap(oldbitmap.PixelWidth, oldbitmap.PixelHeight);

            for (int x = 1; x < oldbitmap.PixelWidth; x++)
            {
                for (int y = 1; y < oldbitmap.PixelHeight; y++)
                {
                    int r, g, b;
                    int curColor = ImageData[y * oldbitmap.PixelWidth + x];
                    byte RedValue = (byte)(curColor >> 16 & 0xFF);
                    byte GreenValue = (byte)(curColor >> 8 & 0xFF);
                    byte BlueValue = (byte)(curColor & 0xFF);
                    //底片效果的处理公式
                    r = 255 - RedValue;
                    g = 255 - GreenValue;
                    b = 255 - BlueValue;

                    byte[] GrayValueArr = new byte[4];
                    GrayValueArr[3] = 0xFF;
                    GrayValueArr[2] = (byte)r;
                    GrayValueArr[1] = (byte)g;
                    GrayValueArr[0] = (byte)b;

                    int GrayPixel = BitConverter.ToInt32(GrayValueArr, 0);

                    newbitmap.Pixels[y * oldbitmap.PixelWidth + x] = GrayPixel;
                }
            }
            newbitmap.Invalidate();
            image1.Source = newbitmap;


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