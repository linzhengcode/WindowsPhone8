using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GalaSoft.MvvmLight.Ioc;
using RssReaderDemo.Models;
using Microsoft.Phone.Tasks;

namespace RssReaderDemo
{
    public partial class DetailsPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null
                && NavigationContext.QueryString.ContainsKey("item"))
            {
                var url = HttpUtility.UrlEncode(NavigationContext.QueryString["item"]);

                if (!SimpleIoc.Default.IsRegistered<RssArticle>(url))
                {
                    MessageBox.Show("Item not found");
                    return;
                }

                var vm = SimpleIoc.Default.GetInstance<RssArticle>(url);
                DataContext = vm;
                SimpleIoc.Default.Unregister(vm);
            }
        }

        private void SeeArticleClick(object sender, RoutedEventArgs e)
        {
            var vm = (RssArticle)DataContext;

            if (vm == null)
            {
                return;
            }

            var task = new WebBrowserTask
            {
                Uri = vm.Link
            };

            task.Show();
        }
    }
}