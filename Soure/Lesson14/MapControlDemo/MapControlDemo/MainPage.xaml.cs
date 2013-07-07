using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MapControlDemo.Resources;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;

namespace MapControlDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public GeoCoordinate currentGeoCoordinate;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            BuildLocalizedApplicationBar();
        }

        private void myMapControl_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "ApplicationID";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "AuthenticationToken";
            GetCoordinates();
        }

        private async void GetCoordinates()
        {
            Geolocator MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracyInMeters = 5;
            Geoposition MyGeoPosition = null;
            try
            {
                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                currentGeoCoordinate = ConvertGeocoordinate(MyGeoPosition.Coordinate);

                myMap.Center = currentGeoCoordinate;
                myMap.ZoomLevel = 20;

                Ellipse myEllipse = new Ellipse();
                myEllipse.Fill = new SolidColorBrush(Colors.Red);
                myEllipse.Height = 30;
                myEllipse.Width = 30;

                MapOverlay mapOverlay = new MapOverlay();
                mapOverlay.Content = myEllipse;
                mapOverlay.PositionOrigin = new Point(0.5, 0.5);
                mapOverlay.GeoCoordinate = currentGeoCoordinate;

                MapLayer mapLayer = new MapLayer();
                mapLayer.Add(mapOverlay);

                myMap.Layers.Add(mapLayer);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings");
            }
            catch (Exception ex)
            {
            }  
        }


        public  GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {
            return new GeoCoordinate
                (
                geocoordinate.Latitude,
                geocoordinate.Longitude,
                geocoordinate.Altitude ?? Double.NaN,
                geocoordinate.Accuracy,
                geocoordinate.AltitudeAccuracy ?? Double.NaN,
                geocoordinate.Speed ?? Double.NaN,
                geocoordinate.Heading ?? Double.NaN
                );
        }
        // 用于生成本地化 ApplicationBar 的示例代码
        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();
            // 使用 AppResources 中的本地化字符串创建新菜单项。
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("改变图层");
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            myMap.CartographicMode = Microsoft.Phone.Maps.Controls.MapCartographicMode.Aerial;
        }
    }
}