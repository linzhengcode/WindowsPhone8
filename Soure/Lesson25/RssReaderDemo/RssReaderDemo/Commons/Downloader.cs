using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.Commons
{
    public class Downloader
    {
        public Task<Stream> GetRssFeed(string feedUrl)
        {
            var tcs = new TaskCompletionSource<Stream>();

            var client = new WebClient();
            client.OpenReadCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    tcs.SetResult(e.Result);
                }
                else
                {
                    tcs.TrySetException(e.Error);
                }
            };

            client.OpenReadAsync(new Uri(feedUrl));

            return tcs.Task;
        }
    }
}
