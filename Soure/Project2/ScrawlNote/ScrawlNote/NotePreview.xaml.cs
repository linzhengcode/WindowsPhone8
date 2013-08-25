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
using ScrawlNote.Controls;
using ScrawlNote.Models.DB;
using System.Windows.Media;
using ScrawlNote.Resources;

namespace ScrawlNote
{
    public partial class NotePreview : PhoneApplicationPage
    {
        public NotePreview()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Images/appbar.edit.rest.png", UriKind.Relative));
            appBarButton.Text = AppResources.Edit;
            appBarButton.Click += ApplicationBarIconButton_Click_1;
            ApplicationBar.Buttons.Add(appBarButton);

            ApplicationBarIconButton appBarButton2 = new ApplicationBarIconButton(new Uri("/Images/pushpin.png", UriKind.Relative));
            appBarButton2.Text = AppResources.Pin;
            appBarButton2.Click += ApplicationBarIconButton_Click_2;
            ApplicationBar.Buttons.Add(appBarButton2);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int id = NavigationHelper.NavigationExtGetIntValue("id");
            if (id > 0)
            {
                NotePreviewViewModel notePreviewViewModel = new NotePreviewViewModel(id);
                if (notePreviewViewModel.Note != null)
                {
                    ShowBody(notePreviewViewModel.Note);
                }
                base.DataContext = notePreviewViewModel;
            }
            else
            {
                id = NavigationHelper.NavigationQueryGetIntValue(base.NavigationContext, "idNote");
                if (id > 0)
                {
                    NotePreviewViewModel notePreviewViewModel = new NotePreviewViewModel(id);
                    if (notePreviewViewModel.Note != null)
                    {
                        ShowBody(notePreviewViewModel.Note);
                    }
                    base.DataContext = notePreviewViewModel;
                }
            }


            base.OnNavigatedTo(e);
        }

        private void ShowBody(Note note)
        {
            this.body.Children.Clear();
            foreach (NoteDetail dett in note.Body)
            {
                if (!string.IsNullOrEmpty(dett.Text))
                {
                    TextBlock block2 = new TextBlock
                    {
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 40,
                        Foreground = new SolidColorBrush(Colors.Black),
                        Text = dett.Text
                    };
                    TextBlock block = block2;
                    this.body.Children.Add(block);
                }
                else if ((dett.ListPageDraw != null) && (dett.ListPageDraw.Count > 0))
                {
                    ShowDrawingsControl drawings2 = new ShowDrawingsControl
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        DataSource = dett.ListPageDraw
                    };
                    ShowDrawingsControl drawings = drawings2;
                    this.body.Children.Add(drawings);
                }
            }

        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NotePreviewViewModel preview = base.DataContext as NotePreviewViewModel;
            NavigationHelper.NavigateExt(base.NavigationService, "/AddEditNote.xaml", "id", preview.Note.Id);
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            ArrangeTile((base.DataContext as NotePreviewViewModel).Note);
        }

        private void ArrangeTile(Note nota)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault<ShellTile>(x => x.NavigationUri.ToString().Contains(string.Format("idNote={0}", nota.Id)));
            string str = nota.Id.ToString();
            StandardTileData data2 = new StandardTileData();
            data2.Title = nota.Title;
            StandardTileData data = data2;
            if (tile != null)
            {
                tile.Update(data);
            }
            else if (tile == null)
            {
                ShellTile.Create(new Uri(string.Format("/NotePreview.xaml?idNote={0}", nota.Id.ToString()), UriKind.Relative), data);
            }
        }
    }
}