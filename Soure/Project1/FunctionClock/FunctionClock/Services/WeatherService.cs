using FunctionClock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Devices.Geolocation;

namespace FunctionClock.Services
{
    public class WeatherService
    {
        private const string WEATHERURI = "http://forecast.weather.gov/MapClick.php";
        private static WeatherService _weatherService;
        private Action<Weather> responseWeather;

        public static WeatherService Current
        {
            get
            {
                if (_weatherService == null)
                {
                    _weatherService = new WeatherService();
                }
                return _weatherService;
            }
        }

        private WeatherService()
        {
        }

        public async void LoadWeatherData(Action<Weather> response)
        {
            responseWeather = response;
            Geoposition geoposition = null;
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.Default;
            try
            {
                geoposition = await geolocator.GetGeopositionAsync();

            }
            catch (Exception e)
            {
            }

            if (geoposition != null)
            {
                LoadWeatherData(geoposition.Coordinate, response); 
            }
            else
            {
                responseWeather(null);
            }
        }

        private void LoadWeatherData(Geocoordinate geo, Action<Weather> response)
        {
            LoadWeatherData(new Uri(WEATHERURI + "?lat=" + geo.Latitude + "&lon=" + geo.Longitude + "&FcstType=dwml"));
        }

        private void LoadWeatherData(Uri uri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(uri);
        }

        void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    Weather weather = new Weather();
                    XElement xmlWeather = XElement.Parse(e.Result);
                    string area = (string)xmlWeather.Descendants("location").First().Element("city");
                    if (area == null || area == "")
                    {
                        area = (string)xmlWeather.Descendants("location").First().Element("area-description");
                    }

                    XElement xml = xmlWeather.Descendants("data").Where(element => element.Attribute("type").Value == "current observations").First();
                    int temp = int.Parse(xml.Descendants("temperature").First().Value);
                    weather.Area = area;
                    weather.Temp = temp;

                    responseWeather(weather);
                }
                catch (Exception err)
                {
                    responseWeather(null);
                }
            }
            else
            {
                responseWeather(null);
            }
        }




    }
}
