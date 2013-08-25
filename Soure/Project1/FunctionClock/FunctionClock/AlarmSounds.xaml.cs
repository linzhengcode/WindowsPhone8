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
    public partial class AlarmSounds : PhoneApplicationPage
    {
        public AlarmSounds()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((AlarmSoundsViewModel)base.DataContext).LoadDataCommand.Execute(null);
            base.OnNavigatedTo(e);
        }
    }
}