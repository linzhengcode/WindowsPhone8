using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScrawlNote.Resources;
using ScrawlNote.ViewModels;
using ScrawlNote.Models;
using ScrawlNote.Commons;
using System.ComponentModel;

namespace ScrawlNote
{
    public partial class DrawPage : PhoneApplicationPage
    {
        public DrawPage()
        {
            InitializeComponent();
            // 用于本地化 ApplicationBar 的示例代码
            BuildLocalizedApplicationBar();
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/appbar.check.rest.png", UriKind.Relative));
            appBarButton.Text = "确定";
            appBarButton.Click += appBarButton_Click;
            ApplicationBar.Buttons.Add(appBarButton);

            ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Images/appbar.back.rest.png", UriKind.Relative));
            appBarButton2.Text = "上一";
            appBarButton2.Click += appBarButton2_Click;
            ApplicationBar.Buttons.Add(appBarButton2);

            ApplicationBarIconButton appBarButton3 = new ApplicationBarIconButton(new Uri("/Images/appbar.next.rest.png", UriKind.Relative));
            appBarButton3.Text = "下一";
            appBarButton3.Click += appBarButton3_Click;
            ApplicationBar.Buttons.Add(appBarButton3);

            // 使用 AppResources 中的本地化字符串创建新菜单项。
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //appBarMenuItem.Click += appBarMenuItem_Click;
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarButton3_Click(object sender, EventArgs e)
        {
            draw.NextPage();
        }

        void appBarButton2_Click(object sender, EventArgs e)
        {
            draw.PreviousPage();
        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            (base.DataContext as DrawViewModel).CurrentList = this.draw.GetListPage().ToList<DrawModel>();
            NavigationHelper.NavigateGoBackExt(base.NavigationService, "vmNew", base.DataContext);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.draw.Undo();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.draw.Rendo();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.draw.Clear();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            (base.DataContext as DrawViewModel).IsAbort = true;
            NavigationHelper.NavigateGoBackExt(base.NavigationService, "vmNew", base.DataContext);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DrawViewModel draw = NavigationHelper.NavigationExtGetValue<DrawViewModel>("vmNew");
            if (draw == null)
            {
                throw new Exception("set viewmodel");
            }
            base.DataContext = draw;
            base.OnNavigatedTo(e);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.draw.SetListPage((base.DataContext as DrawViewModel).CurrentList);
        }
    }
}