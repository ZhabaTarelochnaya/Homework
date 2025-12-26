using System;
using System.Collections.Generic;
using _RSS.Scripts.Data;

namespace _RSS.Scripts.UI
{
    public class RSSFeedEvents
    {
        public event Action<List<NewsItem>> NewsLoaded;
        public event Action<string> LoadRequested;

        public void LoadNews(List<NewsItem> news)
        {
            NewsLoaded?.Invoke(news);
        }

        public void RequestLoadNews(string path)
        {
            LoadRequested?.Invoke(path);
        }
    }
}