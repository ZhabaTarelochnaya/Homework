using System;
using _RSS.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _RSS.Scripts.UI
{
    public class NewsItemView : MonoBehaviour
    {
        readonly DateTime _defaultDate = new (1601, 1, 1);
        [SerializeField] TMP_Text _title;
        [SerializeField] TMP_Text _content;
        [SerializeField] TMP_Text _data;
        
        public void Bind(NewsItem newsItem)
        {
            _title.text = newsItem.Title == "" ? "No title" : newsItem.Title;
            _content.text = newsItem.Content ==  "" ? "No content" : newsItem.Content;
            _data.text = newsItem.Timestamp == _defaultDate ? "No date" : newsItem.Timestamp.ToShortDateString();
        }
    }
}