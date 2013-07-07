using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MapLineDemo
{
    public partial class ShowDestination : PhoneApplicationPage
    {
        public ShowDestination()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.Keys.Contains("latitude"))
            {
                info.Children.Add(new TextBlock { Text = "latitude:" + NavigationContext.QueryString["latitude"].ToString() });
            }
            base.OnNavigatedTo(e);
        }
    }
}