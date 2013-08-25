using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Text;
using System.Threading;

namespace HttpDemo
{
    public partial class Page1 : PhoneApplicationPage
    {
        HttpWebRequest request;
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HtttpPost();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (request != null)
            {
                request.Abort();
            }
            base.OnNavigatingFrom(e);
        }


        private void HtttpPost()
        {
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create("http://dldir1.qq.com/qqfile/qq/QQ2013/2013Beta5/6970/QQ2013Beta5.exe");
                //Thread.Sleep(1000);
                request.Method = "POST";
                request.BeginGetRequestStream(ResponseStreamCallbackPost, request);
                //request.BeginGetResponse(ResponseCallbackPost, request);
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
        }

        private void ResponseStreamCallbackPost(IAsyncResult result)
        {
            try
            {
                
                HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
                Stream stream = httpWebRequest.EndGetRequestStream(result);

                string postString = "qqqqqqqqq";
                byte[] data = Encoding.UTF8.GetBytes(postString);
                stream.Write(data, 0, data.Length);
                stream.Close();
                httpWebRequest.BeginGetResponse(ResponseCallbackPost, httpWebRequest);
                
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
            //using (Stream stream = webResponse.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    string content = reader.ReadToEnd();
            //    Dispatcher.BeginInvoke(() => MessageBox.Show(content));
            //}
        }

        private void ResponseCallbackPost(IAsyncResult result)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
                WebResponse webResponse = httpWebRequest.EndGetResponse(result);
                using (Stream stream = webResponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    Dispatcher.BeginInvoke(() => MessageBox.Show(content));
                }
                request = null;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
        }
    }
}