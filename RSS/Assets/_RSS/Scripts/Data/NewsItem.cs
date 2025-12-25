#nullable enable
using System;

namespace _RSS.Scripts.Data
{
    public class NewsItem
    {
        public string? Title { get; }
        public string? Content { get; }
        public DateTime Timestamp { get; }
        public NewsItem(NewsItemData newsItemData)
        {
            Title = newsItemData.Title;
            Content = newsItemData.Content;
            Timestamp = newsItemData.Timestamp;
        }
    }
}