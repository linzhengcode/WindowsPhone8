using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DouBanRxDemo.Resources;
using System.Xml.Linq;
using Microsoft.Phone.Reactive;
using System.IO;

namespace DouBanRxDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static string _atomNamespace = "http://www.w3.org/2005/Atom";

        private static XName _entryName = XName.Get("entry", _atomNamespace);

        private static XName _titleName = XName.Get("title", _atomNamespace);

        private static XName _idName = XName.Get("id", _atomNamespace);

        private static XName _publishedName = XName.Get("author", _atomNamespace);

        private static XName _nameName = XName.Get("name", _atomNamespace);

        public MainPage()
        {
            InitializeComponent();

            Func<string, IObservable<string>> search = searchText =>
            {
                var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(string.Format("http://api.douban.com/book/subjects?tag={0}&max-results=20",searchText)));
                var bookSearch = Observable.FromAsyncPattern<WebResponse>(request.BeginGetResponse, request.EndGetResponse);
                return bookSearch().Select(res => WebResponseToString(res));
            };
            Observable.FromEvent<TextChangedEventArgs>(searchTextBox, "TextChanged")
                .Select(e => (e.Sender as TextBox).Text)
                .Where(text => text.Length > 2)
                .Do(s => searchResults.Opacity = 0.5)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .ObserveOnDispatcher()
                .Do(s => LoadingIndicator.Visibility = Visibility.Visible)
                .SelectMany(txt => search(txt))
                .Select(searchRes => ParseSearch(searchRes))
                .ObserveOnDispatcher()
                .Do(s => LoadingIndicator.Visibility = Visibility.Collapsed)
                .Do(s => searchResults.Opacity = 1)
                .Subscribe(soure => searchResults.ItemsSource = soure);


        }
        /// <summary>
        /// 处理网络请求的回应
        /// </summary>
        /// <param name="webResponse">网络回应</param>
        /// <returns>返回的字符串</returns>
        private string WebResponseToString(WebResponse webResponse)
        {
            HttpWebResponse response = (HttpWebResponse)webResponse;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// 解析XML文件
        /// </summary>
        /// <param name="response">xml文件字符串</param>
        /// <returns>返回书籍的枚举集合</returns>
        private IEnumerable<Book> ParseSearch(string response)
        {
            int i=1;
            var doc = XDocument.Parse(response);
            return doc.Descendants(_entryName)
                      .Select(entryElement => new Book()
                      {
                          Num = i++,
                          Title = entryElement.Descendants(_titleName).Single().Value,
                          Id = entryElement.Descendants(_idName).Single().Value,
                          Author = entryElement.Descendants(_nameName).Count()>0 ? entryElement.Descendants(_nameName).First().Value : ""
                      });
        }
    }
    /// <summary>
    /// 书的实体类
    /// </summary>
    public class Book
    {
        public int Num { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}