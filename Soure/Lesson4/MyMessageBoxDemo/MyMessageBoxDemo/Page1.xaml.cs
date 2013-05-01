using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace MyMessageBoxDemo
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            string key = this.NavigationContext.QueryString["key"].ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string key = this.NavigationContext.QueryString["key"].ToString();
            Debug.WriteLine("Page1 OnNavigatedTo");
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Debug.WriteLine("Page1 OnBackKeyPress");
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }
    }
}