using FunctionClock.Models;
using FunctionClock.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FunctionClock.ViewModels
{
    public class WeatherViewModel : ViewModelBase
    {
        public WeatherViewModel()
        {
            ShowWeaher = Visibility.Collapsed;
        }

        public void LoadWearherData()
        {
            Task.Factory.StartNew(() =>
                {
                    WeatherService.Current.LoadWeatherData(w =>
                        {
                            Deployment.Current.Dispatcher.BeginInvoke(() =>
                                {
                                    if (w == null)
                                    {
                                        ShowWeaher = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        ShowWeaher = Visibility.Visible;
                                        Weather = w;
                                    }
                                });
                        });
                });
        }

        private Visibility _showWeaher;
        public Visibility ShowWeaher
        {
            get
            {
                return _showWeaher;
            }
            set
            {
                _showWeaher = value;
                this.RaisePropertyChanged("ShowWeaher");
            }
        }

        private Weather _weather;
        public Weather Weather
        {
            get
            {
                return _weather;
            }
            set
            {
                _weather = value;
                this.RaisePropertyChanged("Weather");
            }
        } 
    }
}
