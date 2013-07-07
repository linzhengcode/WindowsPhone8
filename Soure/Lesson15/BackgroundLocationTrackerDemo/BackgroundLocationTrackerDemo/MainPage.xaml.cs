using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BackgroundLocationTrackerDemo.Resources;
using System.Device.Location;
using Windows.Devices.Geolocation;

namespace BackgroundLocationTrackerDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public GeoCoordinate currentGeoCoordinate = null;
        public GeoCoordinate comeinGeoCoordinate = null;
        Geolocator MyGeolocator;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected override void OnRemovedFromJournal(JournalEntryRemovedEventArgs e)
        {
            if (MyGeolocator != null)
            {
                MyGeolocator.StatusChanged -= MyGeolocator_StatusChanged;
                MyGeolocator.PositionChanged -= MyGeolocator_PositionChanged;
                MyGeolocator = null;
            }
            base.OnRemovedFromJournal(e);
        }

        private void myMap_Loaded_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = "ApplicationID";
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = "AuthenticationToken";
            GetCoordinates();
        }


        private async void GetCoordinates()
        {
            MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracy = PositionAccuracy.High;
            MyGeolocator.MovementThreshold = 10; // 单位米
            Geoposition MyGeoPosition = null;
            try
            {
                MyGeolocator.StatusChanged += MyGeolocator_StatusChanged;
                MyGeolocator.PositionChanged += MyGeolocator_PositionChanged;

                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                currentGeoCoordinate = ConvertGeocoordinate(MyGeoPosition.Coordinate);

                myMap.Center = currentGeoCoordinate;
                myMap.ZoomLevel = 15;


            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings");
            }
            catch (Exception ex)
            {
            }
        }

        void MyGeolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (App.RunningInBackground == true)
            {
                ShellToast toast = new ShellToast();
                toast.Content = args.Position.Coordinate.Latitude + " " + args.Position.Coordinate.Longitude;
                toast.Title = "位置变化";
                toast.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                toast.Show();
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    GeoCoordinate geoCoordinate = new GeoCoordinate { Latitude = args.Position.Coordinate.Latitude, Longitude = args.Position.Coordinate.Longitude };
                    myMap.Center = geoCoordinate;
                });
            }
          
        }

        void MyGeolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";

            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    status = "Disabled";
                    break;
                case PositionStatus.Initializing:
                    status = "initializing";
                    break;
                case PositionStatus.NoData:
                    status = "no data";
                    break;
                case PositionStatus.Ready:
                    status = "ready";
                    break;
                case PositionStatus.NotInitialized:
                    status = "NotInitialized";
                    break;
            }
            if (App.RunningInBackground == true)
            {
                ShellToast toast = new ShellToast();
                toast.Content =status;
                toast.Title = "状态发生了变化";
                toast.NavigationUri = new Uri("/MainPage.xaml", UriKind.Relative);
                toast.Show();
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(status);
                });
            }
        }


        public GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
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