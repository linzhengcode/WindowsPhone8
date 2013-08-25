using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScrawlNote.Commons;
using ScrawlNote.ViewModels;
using System.ComponentModel;

namespace ScrawlNote
{
    public partial class TextPage : PhoneApplicationPage
    {
        public TextPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/appbar.save.rest.png", UriKind.Relative));
            appBarButton.Text = "保存";
            appBarButton.Click += ApplicationBarIconButton_Click;
            ApplicationBar.Buttons.Add(appBarButton);

            ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Images/appbar.delete.rest.png", UriKind.Relative));
            appBarButton2.Text = "删除";
            appBarButton2.Click += ApplicationBarIconButton_Click_1;
            ApplicationBar.Buttons.Add(appBarButton2);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationHelper.NavigateGoBackExt(base.NavigationService, "vmNew", base.DataContext);
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除?", string.Empty, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                (base.DataContext as TextViewModel).IsDelete = true;
                NavigationHelper.NavigateGoBackExt(base.NavigationService, "vmNew", base.DataContext);
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            (base.DataContext as TextViewModel).IsAbort = true;
            NavigationHelper.NavigateGoBackExt(base.NavigationService, "vmNew", base.DataContext);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextViewModel text = NavigationHelper.NavigationExtGetValue<TextViewModel>("vmNew");
            base.DataContext = text;
            base.OnNavigatedTo(e);
        }
    }
}