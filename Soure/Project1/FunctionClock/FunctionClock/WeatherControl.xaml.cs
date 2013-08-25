using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FunctionClock.ViewModels;

namespace FunctionClock
{
    public partial class WeatherControl : UserControl
    {
        public WeatherControl()
        {
            InitializeComponent();
        }

        public void RefreshWeatherData()
        {
            ((WeatherViewModel)base.DataContext).LoadWearherData();
        }
    }
}
