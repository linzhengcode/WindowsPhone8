using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using XMLLINQDemo.Resources;
using System.Xml.Linq;
using System.Windows.Resources;
using System.Diagnostics;

namespace XMLLINQDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        XElement tests;
        public MainPage()
        {
            InitializeComponent();
            StreamResourceInfo xml = Application.GetResourceStream(new Uri("/XMLLINQDemo;component/XMLFile1.xml", System.UriKind.Relative));
            tests = XElement.Load(xml.Stream);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int i=0;
            var mysource = from item in tests.Elements("Test") where item.Attribute("Name").Value.Length>4 select new MyTest { Name = item.Attribute("Name").Value, Number = i++ };
            listbox1.ItemsSource = mysource;

        }

    }

    class MyTest
    {
        public string Name {get;set;}
        public int Number { get; set; } 
    }
}