using System.Collections.Generic;

namespace _RSS.Scripts.Data
{
    public class News
    {
        public List<NewsItem> NewsItems { get; }

        public News(List<NewsItem> newsItems) => NewsItems = newsItems;
        public News() => NewsItems = new List<NewsItem>();
    }
}