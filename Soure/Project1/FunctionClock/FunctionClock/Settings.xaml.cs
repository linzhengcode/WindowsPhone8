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
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ((SettingViewModel)base.DataContext).SaveCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }
    }
}