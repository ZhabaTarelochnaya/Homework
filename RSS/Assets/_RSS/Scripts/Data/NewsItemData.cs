#nullable enable
using System;
using _RSS.Scripts.Utils;

namespace _RSS.Scripts.Data
{
    [Serializable]
    public class NewsItemData
    {
        public string? Title;
        public string? Content;
        public JsonDateTime Timestamp;
    }
}