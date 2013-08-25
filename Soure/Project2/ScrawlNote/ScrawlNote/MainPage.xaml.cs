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
using ScrawlNote.Commons;
using ScrawlNote.Models.DB;

namespace ScrawlNote
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            BuildLocalizedApplicationBar();
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();

            // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/appbar.add.rest.png", UriKind.Relative));
            appBarButton.Click += appBarButton_Click;
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Images/appbar.feature.search.rest.png", UriKind.Relative));
            appBarButton2.Click += appBarButton2_Click;
            appBarButton2.Text = AppResources.Search;
            ApplicationBar.Buttons.Add(appBarButton2);

            // 使用 AppResources 中的本地化字符串创建新菜单项。
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.SettingTitle);
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarButton2_Click(object sender, EventArgs e)
        {
            Visibility visibility = (Visibility)base.Resources["PhoneLightThemeVisibility"];
            if (this.pivot.SelectedIndex == 1)
            {
                if (this.searchTxt.Visibility == Visibility.Collapsed)
                {
                    if (visibility == Visibility.Visible)
                    {
                        this.searchImLight.Visibility = Visibility.Visible;
                        this.searchIm.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.searchImLight.Visibility = Visibility.Collapsed;
                        this.searchIm.Visibility = Visibility.Visible;
                    }
                    this.searchTxt.Visibility = Visibility.Visible;
                    this.searchTxt.Focus();
                }
                else
                {
                    this.searchIm.Visibility = Visibility.Collapsed;
                    this.searchImLight.Visibility = Visibility.Collapsed;
                    this.searchTxt.Visibility = Visibility.Collapsed;
                    this.searchTxt.Text = string.Empty;
                    (base.DataContext as MainViewModel).Read(this.GetPreferiti(), string.Empty);
                }
            }
            else if (this.pivot.SelectedIndex == 0)
            {
                if (this.searchTxt2.Visibility == Visibility.Collapsed)
                {
                    if (visibility == Visibility.Visible)
                    {
                        this.searchIm2Light.Visibility = Visibility.Visible;
                        this.searchIm2.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.searchIm2Light.Visibility = Visibility.Collapsed;
                        this.searchIm2.Visibility = Visibility.Visible;
                    }
                    this.searchTxt2.Visibility = Visibility.Visible;
                    this.searchTxt2.Focus();
                }
                else
                {
                    this.searchIm2.Visibility = Visibility.Collapsed;
                    this.searchIm2Light.Visibility = Visibility.Collapsed;
                    this.searchTxt2.Visibility = Visibility.Collapsed;
                    this.searchTxt2.Text = string.Empty;
                    (base.DataContext as MainViewModel).Read(this.GetPreferiti(), string.Empty);
                }
            }
        }

        void appBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Setting.xaml", UriKind.Relative));
        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddEditNote.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainViewModel main = null;
            if (this.searchTxt.Visibility == Visibility.Visible)
            {
                main = new MainViewModel(this.searchTxt.Text, this.searchTxt2.Text);
            }
            else
            {
                main = new MainViewModel();
            }
            base.DataContext = main;
            base.OnNavigatedTo(e);
        }

        private void PostItControl_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Note note = (sender as StackPanel).DataContext as Note;
            NavigationHelper.NavigateExt(base.NavigationService, "/NotePreview.xaml", "id", note.Id);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (base.DataContext as MainViewModel).Read(this.GetPreferiti(), (sender as TextBox).Text);
        }

        private bool GetPreferiti()
        {
            return (this.pivot.SelectedIndex == 1);
        }
    }
}