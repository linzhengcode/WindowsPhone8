using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HttpDemo.Resources;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        string url = "http://dldir1.qq.com/qqfile/qq/QQ2013/2013Beta5/6970/QQ2013Beta5.exe";
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HtttpGet();
        }

        private void HtttpGet()
        {
            var request = HttpWebRequest.Create("http://www.baidu.com");
            request.Method = "GET";

            request.Headers["Cookie"] = "name=value";

            request.Credentials = new NetworkCredential("accountKey", "accountkeyOrPassword");

            request.BeginGetResponse(ResponseCallback, request);
        }

        private void ResponseCallback(IAsyncResult result)
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
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HtttpPost();
        }

        private void HtttpPost()
        {
            try
            {
                var request = HttpWebRequest.Create("http://www.cnblogs.com");
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
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }
        private AutoResetEvent autoResetEvent;
        HttpWebRequest request;
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            autoResetEvent = new AutoResetEvent(false);

            Task.Factory.StartNew(HtttpGetTimeTest);
            autoResetEvent.WaitOne(3000);
            if (request != null)
            {
                request.Abort();
            }
            MessageBox.Show("请求超时");

        }

        private void HtttpGetTimeTest()
        {
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.BeginGetResponse(ResponseCallbackTimeTest, request);

            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {

                }
                Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message + " Status:" + e.Status));
            }
        }

        private void ResponseCallbackTimeTest(IAsyncResult result)
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Headers["Range"] = "bytes=0-100";
                request.BeginGetResponse(ResponseCallbackTimeTest, request);

            }
            catch (WebException ee)
            {
                if (ee.Status == WebExceptionStatus.Timeout)
                {

                }
                MessageBox.Show(ee.Message + " Status:" + ee.Status);
            }
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}