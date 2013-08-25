using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.Models
{
    public class RssArticle
    {
        public Uri Link
        {
            get;
            set;
        }

        public string Summary
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public RssArticle()
        {
        }
    }
}
