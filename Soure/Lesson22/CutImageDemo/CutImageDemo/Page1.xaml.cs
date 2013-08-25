using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.IO;

namespace CutImageDemo
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream stream = new IsolatedStorageFileStream("myImage.jpg", FileMode.Open, file);

            BitmapImage image = new BitmapImage();
            image.SetSource(stream);

            image1.Source = image;
            image1.Height = image.PixelHeight;
            image1.Width = image.PixelWidth;
        }
    }
}