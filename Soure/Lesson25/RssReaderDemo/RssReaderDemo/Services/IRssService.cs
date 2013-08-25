using RssReaderDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.Services
{
    public interface IRssService
    {
        Task<IList<RssArticle>> GetArticles();
    }
}
