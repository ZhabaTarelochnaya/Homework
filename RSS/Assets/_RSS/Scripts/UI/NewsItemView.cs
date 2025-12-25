using System;
using _RSS.Scripts.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace _RSS.Scripts.UI
{
    public class NewsItemView : MonoBehaviour
    {
        [SerializeField] TMP_Text _title;
        [SerializeField] TMP_Text _content;
        [SerializeField] TMP_Text _data;
        
        public void Bind(NewsItem newsItem)
        {
            _title.text = newsItem.Title == "" ? "No title" : newsItem.Title;
            _content.text = newsItem.Content ==  "" ? "No content" : newsItem.Content;
            _data.text = newsItem.Timestamp == DateTime.MinValue ? "No date" : newsItem.Timestamp.ToShortDateString();
        }
    }
}