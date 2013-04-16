using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ObservableWebDemo.Resources;
using System.IO;
using System.Text;
using Microsoft.Phone.Reactive;

namespace ObservableWebDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.cnblogs.com/linzheng");
            request.Method = "GET";
            request.BeginGetResponse(new AsyncCallback(GetResponse), request);
        }

        void GetResponse(IAsyncResult res)
        {
            HttpWebRequest request = (HttpWebRequest)res.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(res);
            string content = "";
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                MessageBox.Show(content);
            });
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.cnblogs.com/linzheng");
            request.Method = "GET";
            Observable.FromAsyncPattern<WebResponse>(request.BeginGetResponse, request.EndGetResponse)()
                .Subscribe(delegate(WebResponse res)
                {
                    string content = "";
                    using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                    Deployment.Current.Dispatcher.BeginInvoke(delegate
                    {
                        MessageBox.Show(content);
                    });
                });
        }

    }
}