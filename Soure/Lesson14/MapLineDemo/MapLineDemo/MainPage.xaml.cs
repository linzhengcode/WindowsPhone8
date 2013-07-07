using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MapLineDemo.Resources;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using System.Diagnostics;

namespace MapLineDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public GeoCoordinate currentGeoCoordinate = null;
        public GeoCoordinate comeinGeoCoordinate = null;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            double latitude=double.NaN;
            double longitude = double.NaN;
            if (NavigationContext.QueryString.Keys.Contains("latitude"))
            {
                latitude = Double.Parse(NavigationContext.QueryString["latitude"].ToString());
            }
            if (NavigationContext.QueryString.Keys.Contains("longitude"))
            {
                longitude = Double.Parse(NavigationContext.QueryString["longitude"].ToString());
            }
            if (latitude != double.NaN && longitude != double.NaN)
            {
                comeinGeoCoordinate = new GeoCoordinate { Latitude = latitude, Longitude = longitude };
            }
            base.OnNavigatedTo(e);
        }

        private void myMap_Loaded_1(object sender, RoutedEventArgs e)
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
                myMap.ZoomLevel = 15;

                if (!comeinGeoCoordinate.IsUnknown)
                {
                    List<GeoCoordinate> geoCoordinates = new List<GeoCoordinate>();
                    geoCoordinates.Add(currentGeoCoordinate);
                    geoCoordinates.Add(comeinGeoCoordinate);

                    RouteQuery routeQuery = new RouteQuery();
                    routeQuery.Waypoints = geoCoordinates;
                    routeQuery.QueryCompleted += routeQuery_QueryCompleted;
                    routeQuery.QueryAsync();
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings");
            }
            catch (Exception ex)
            {
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
        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();
            // 使用 AppResources 中的本地化字符串创建新菜单项。
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem("查询目的地");
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGeoCoordinate != null)
            {
                GeocodeQuery geocodeQuery = new GeocodeQuery();
                geocodeQuery.SearchTerm = "北京";
                geocodeQuery.GeoCoordinate = currentGeoCoordinate;
                geocodeQuery.QueryCompleted += geocodeQuery_QueryCompleted;
                geocodeQuery.QueryAsync();
            }
        }

        void geocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                GeoCoordinate geoCoordinate = e.Result[0].GeoCoordinate;
                myMap.Center = e.Result[0].GeoCoordinate;
                List<GeoCoordinate> geoCoordinates=new List<GeoCoordinate>();
                geoCoordinates.Add(e.Result[0].GeoCoordinate);
                geoCoordinates.Add(e.Result[1].GeoCoordinate);

                RouteQuery routeQuery = new RouteQuery();
                routeQuery.Waypoints = geoCoordinates;
                routeQuery.QueryCompleted += routeQuery_QueryCompleted;
                routeQuery.QueryAsync();
            }
        }

        void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                Route route = e.Result;
                MapRoute mapRoute = new MapRoute(route);
                myMap.AddRoute(mapRoute);
                string routeContent = "";
                foreach (RouteLeg leg in route.Legs)
                {
                    foreach (RouteManeuver routeManeuver in leg.Maneuvers)
                    {
                        routeContent += routeManeuver.InstructionText;
                        Debug.WriteLine(routeManeuver.InstructionText);
                    }
                }
                MessageBox.Show(routeContent);
            }
            else
            {
                MessageBox.Show("无法查到路线");
            }
        }
    }
}