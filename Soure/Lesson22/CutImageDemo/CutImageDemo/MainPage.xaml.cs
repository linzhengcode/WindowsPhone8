using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CutImageDemo.Resources;
using System.Windows.Shapes;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.IO.IsolatedStorage;
using System.IO;

namespace CutImageDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        Rectangle r;

        public MainPage()
        {
            InitializeComponent();

            PhotoChooserTask task = new PhotoChooserTask();
            task.Show();
            task.Completed += new EventHandler<PhotoResult>(task_Completed);
        }

        void task_Completed(object sender, PhotoResult e)
        {
            BitmapImage image = new BitmapImage();
            image.SetSource(e.ChosenPhoto);
            image1.Source = image;

            SetPicture();
        }

        void SetPicture()
        {
            Rectangle rect = new Rectangle();
            rect.Opacity = 0.5;
            rect.Fill = new SolidColorBrush(Colors.White);
            rect.Height = image1.Height;
            rect.MaxHeight = image1.Height;
            rect.MaxWidth = image1.Width;
            rect.Width = image1.Width;
            rect.Stroke = new SolidColorBrush(Colors.Red);
            rect.StrokeThickness = 2;
            rect.Margin = image1.Margin;
            rect.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(rect_ManipulationDelta);

            LayoutRoot.Children.Add(rect);
            LayoutRoot.Height = image1.Height;
            LayoutRoot.Width = image1.Width;
        }


        void rect_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Rectangle croppingRectangle = (Rectangle)sender;

            if (croppingRectangle.Width >= (int)e.DeltaManipulation.Translation.X)
                croppingRectangle.Width -= (int)e.DeltaManipulation.Translation.X;

            if (croppingRectangle.Height >= (int)e.DeltaManipulation.Translation.Y)
                croppingRectangle.Height -= (int)e.DeltaManipulation.Translation.Y;
        }

        private void WriteDummyImage(BitmapImage image)
        {
            Image imageC = new Image();
            imageC.Stretch = Stretch.None;
            imageC.Source = image;
            imageC.Height = r.Height;
            imageC.Width = r.Width;

            WriteBitmap(imageC);
        }

        void ClipImage()
        {
            RectangleGeometry geo = new RectangleGeometry();

            r = (Rectangle)(from c in LayoutRoot.Children where c.Opacity == 0.5 select c).First();
            GeneralTransform gt = r.TransformToVisual(LayoutRoot);
            Point p = gt.Transform(new Point(0, 0));
            geo.Rect = new Rect(p.X, p.Y, r.Width, r.Height);
            image1.Clip = geo;
            r.Visibility = System.Windows.Visibility.Collapsed;

            TranslateTransform t = new TranslateTransform();
            t.X = -p.X;
            t.Y = -p.Y;
            image1.RenderTransform = t;
        }

        void WriteBitmap(FrameworkElement element)
        {
            WriteableBitmap wBitmap = new WriteableBitmap(element, null);

            using (MemoryStream stream = new MemoryStream())
            {
                wBitmap.SaveJpeg(stream, (int)element.Width, (int)element.Height, 0, 100);

                using (var local = new IsolatedStorageFileStream("myImage.jpg", FileMode.Create, IsolatedStorageFile.GetUserStoreForApplication()))
                {
                    local.Write(stream.GetBuffer(), 0, stream.GetBuffer().Length);
                }
            }
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            ClipImage();

            WriteBitmap(LayoutRoot);

            var image = new BitmapImage();
            using (var local = new IsolatedStorageFileStream("myImage.jpg", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()))
            {
                image.SetSource(local);
            }

            WriteDummyImage(image);

            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
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