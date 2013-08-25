using RssReaderDemo.Commons;
using RssReaderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssReaderDemo.Services
{
    public class RssService : IRssService
    {
        private const string NewsPageUrl = "http://feed.cnblogs.com/blog/sitehome/rss";

        public async Task<IList<RssArticle>> GetArticles()
        {
            var downloader = new Downloader();
            var feed = await downloader.GetRssFeed(NewsPageUrl);
            List<RssArticle> rssItems = new List<RssArticle>();
            XmlReader response = XmlReader.Create(feed);
            SyndicationFeed feeds = SyndicationFeed.Load(response);
            foreach (SyndicationItem f in feeds.Items)
            {
                RssArticle rssItem = new RssArticle
                {
                    Link = new Uri(f.Links[0].Uri.AbsoluteUri),
                    Title = f.Title.Text,
                    Summary = f.Summary.Text
                };
                rssItems.Add(rssItem);
            }
            return rssItems;
        }
    }
}
