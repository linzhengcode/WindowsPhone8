using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;
using System.IO;
using System.Text;


namespace FileAssociationDemo
{
    public partial class LogPage : PhoneApplicationPage
    {
        public LogPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.Keys.Contains("fileToken"))
            {
                string fileToken = NavigationContext.QueryString["fileToken"];
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync
                    ("log", CreationCollisionOption.OpenIfExists);
                string incomingName = SharedStorageAccessManager.GetSharedFileName(fileToken);
                var file = await SharedStorageAccessManager.CopySharedFileAsync
                    (folder, incomingName, NameCollisionOption.GenerateUniqueName, fileToken);
                var fileStream = await file.OpenReadAsync();
                var stream = fileStream.AsStreamForRead();
                byte[] content = new byte[stream.Length];
                await stream.ReadAsync(content, 0, content.Length);
                string text = Encoding.UTF8.GetString(content, 0, content.Length);
                log.Text = text;
                    //await SharedStorageAccessManager
            }

            base.OnNavigatedTo(e);
        }
    }
}