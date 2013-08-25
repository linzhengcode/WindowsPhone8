using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScrawlNote.ViewModels;
using ScrawlNote.Models;
using ScrawlNote.Commons;
using ScrawlNote.Models.DB;
using System.Windows.Media;
using System.ComponentModel;
using ScrawlNote.Controls;

namespace ScrawlNote
{
    public partial class AddEditNote : PhoneApplicationPage
    {
        public AddEditNote()
        {
            this.InitializeComponent();
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
            AddEditNoteViewModel new2 = base.DataContext as AddEditNoteViewModel;
            if (new2.Save())
            {
                if (new2.StateModel == StateModel.Update)
                {
                    NavigationHelper.NavigateGoBackExt(base.NavigationService, "id", new2.Note.Id);
                }
                else
                {
                    NavigationHelper.NavigateGoBack(base.NavigationService);
                }
            }
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除?", "程序提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                AddEditNoteViewModel new2 = base.DataContext as AddEditNoteViewModel;
                new2.Delete();
                if (new2.StateModel == StateModel.Update)
                {
                    this.DeleteTile(new2.Note);
                    base.NavigationService.RemoveBackEntry();
                }
                NavigationHelper.NavigateGoBack(base.NavigationService, true);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (base.DataContext == null)
            {
                int id = NavigationHelper.NavigationExtGetIntValue("id");
                if (id > 0)
                {
                    base.DataContext = new AddEditNoteViewModel(id);
                    this.AppendAllControlsBody();
                }
                else if (id == 0)
                {
                    AddEditNoteViewModel new2 = new AddEditNoteViewModel
                    {
                        Note = { Favorite = NavigationHelper.NavigationExtGetBoolValue("favorite") }
                    };
                    base.DataContext = new2;
                }
            }
            ViewModelNewBase vm = NavigationHelper.NavigationExtGetValue<ViewModelNewBase>("vmNew");
            if (vm != null)
            {
                this.ArrangeBody(vm);
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
            AddEditNoteViewModel new2 = base.DataContext as AddEditNoteViewModel;
            if (new2.StateModel == StateModel.Update)
            {
                NavigationHelper.NavigateGoBackExt(base.NavigationService, "id", new2.Note.Id);
            }
            else
            {
                NavigationHelper.NavigateGoBack(base.NavigationService);
            }
            base.OnBackKeyPress(e);
        }

        private void sd_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int index = this.body.Children.IndexOf(sender as UIElement);
            ViewModelNewBase vm = (base.DataContext as AddEditNoteViewModel).ListViewModels[index];
            vm.CopyViewModel();
            this.NavigateDraw(vm);
        }

        private void txt_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            int index = this.body.Children.IndexOf(block.Parent as UIElement);
            ViewModelNewBase vm = (base.DataContext as AddEditNoteViewModel).ListViewModels[index];
            vm.CopyViewModel();
            this.NavigateText(vm);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateText(new TextViewModel());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigateDraw(new DrawViewModel());
        }


        private void AppendAllControlsBody()
        {
            AddEditNoteViewModel dataContext = base.DataContext as AddEditNoteViewModel;
            foreach (ViewModelNewBase base2 in dataContext.ListViewModels)
            {
                if (base2 is TextViewModel)
                {
                    this.CreateTextBlock(base2);
                }
                else if (base2 is DrawViewModel)
                {
                    this.CreateDrawPreview(base2);
                }
            }
        }

        private void ArrangeBody(ViewModelNewBase vm)
        {
            int index = (base.DataContext as AddEditNoteViewModel).ListViewModels.IndexOf(vm);
            if (((index == -1) && !vm.IsAbort) && !vm.IsDelete)
            {
                (base.DataContext as AddEditNoteViewModel).ListViewModels.Add(vm);
                if (vm is TextViewModel)
                {
                    this.CreateTextBlock(vm);
                }
                else if (vm is DrawViewModel)
                {
                    this.CreateDrawPreview(vm);
                }
            }
            else if (index >= 0)
            {
                if (vm.IsDelete)
                {
                    (base.DataContext as AddEditNoteViewModel).ListViewModels.RemoveAt(index);
                    this.body.Children.RemoveAt(index);
                }
                else
                {
                    if (vm.IsAbort)
                    {
                        vm.IsAbort = false;
                        vm.RestoreViewModel();
                    }
                    if (vm is TextViewModel)
                    {
                        Border border = this.body.Children[index] as Border;
                        (border.Child as TextBlock).Text = (vm as TextViewModel).CurrentText;
                    }
                    else if (vm is DrawViewModel)
                    {
                        ShowDrawingsControl drawings = this.body.Children[index] as ShowDrawingsControl;
                        drawings.DataSource = (vm as DrawViewModel).CurrentList;
                    }
                }
            }
        }

        
        private void CreateDrawPreview(ViewModelNewBase vm)
        {
            ShowDrawingsControl drawings2 = new ShowDrawingsControl
            {
                Margin = new Thickness(5.0)
            };
            ShowDrawingsControl drawings = drawings2;
            this.body.Children.Add(drawings);
            drawings.Tag = vm;
            drawings.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(this.sd_Tap);
            drawings.HorizontalAlignment = HorizontalAlignment.Stretch;
            drawings.VerticalAlignment = VerticalAlignment.Stretch;
            drawings.Background = new SolidColorBrush(Colors.White);
            drawings.DataSource = (vm as DrawViewModel).CurrentList;
        }

        private void CreateTextBlock(ViewModelNewBase vm)
        {
            TextBlock block2 = new TextBlock
            {
                Text = (vm as TextViewModel).CurrentText,
                FontSize = 46.0,
                Foreground = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap
            };
            TextBlock block = block2;
            block.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(this.txt_Tap);
            Border border2 = new Border
            {
                Margin = new Thickness(5.0)
            };
            Border border = border2;
            border.Child = block;
            this.body.Children.Add(border);
        }

        private void DeleteTile(Note nota)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault<ShellTile>(x => x.NavigationUri.ToString().Contains(string.Format("idNote={0}", nota.Id)));
            if (tile != null)
            {
                tile.Delete();
            }
        }

        private void NavigateDraw(ViewModelNewBase vm)
        {
            NavigationHelper.NavigateExt(base.NavigationService, "/DrawPage.xaml", "vmNew", vm);
        }

        private void NavigateText(ViewModelNewBase vm)
        {
            NavigationHelper.NavigateExt(base.NavigationService, "/TextPage.xaml", "vmNew", vm);
        }
    }
}