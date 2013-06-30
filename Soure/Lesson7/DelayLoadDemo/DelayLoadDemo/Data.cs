using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DelayLoadDemo
{
    class Data: INotifyPropertyChanged
    {
        public string Name { get; set; }

        private Uri imageUri;
        public Uri ImageUri
        {
            get
            {
                return imageUri;
            }
            set
            {
                if (imageUri == value)
                {
                    return;
                }
                imageUri = value;
                bitmapImage = null;
            }
        }
        WeakReference bitmapImage;

        public ImageSource ImageSource
        {

            get
            {
                if (bitmapImage != null)
                {
                    if (bitmapImage.IsAlive)
                        return (ImageSource)bitmapImage.Target;
                    else
                        Debug.WriteLine("数据已经被回收");
                }
                if (imageUri != null)
                {
                    ThreadPool.QueueUserWorkItem(DownloadImage, imageUri);
                }
                return null;
            }
        }

        void DownloadImage(object state)
        {
            HttpWebRequest request = WebRequest.CreateHttp(state as Uri);
            request.BeginGetResponse(DownloadImageComplete, request);
        }

        void DownloadImageComplete(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            Stream stream = response.GetResponseStream();
            int length = (int)response.ContentLength;
            Stream streamForUI = new MemoryStream(length);
            byte[] buffer = new byte[length];
            int read=0;
            do
            {
                read = stream.Read(buffer, 0, length);
                streamForUI.Write(buffer, 0, read);
            } while (read == length);
            streamForUI.Seek(0, SeekOrigin.Begin);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    BitmapImage bm = new BitmapImage();
                    bm.SetSource(streamForUI);

                    if (bitmapImage == null)
                        bitmapImage = new WeakReference(bm);
                    else
                        bitmapImage.Target = bm;
                    //触发UI的改变
                    OnPropertyChanged("ImageSource");
                }
            );
        }

        void OnPropertyChanged(string property)
        {
            var hander = PropertyChanged;
            if (hander != null)
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    hander(this, new PropertyChangedEventArgs(property));
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
