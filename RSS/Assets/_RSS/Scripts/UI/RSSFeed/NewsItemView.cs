using System;
using System.Collections;
using _RSS.Scripts.Data;
using TMPro;
using UnityEngine;

namespace _RSS.Scripts.UI
{
    public class NewsItemView : MonoBehaviour
    {
        readonly DateTime _defaultDate = new (1601, 1, 1);
        [SerializeField] float _showUpTime;
        [SerializeField] TMP_Text _title;
        [SerializeField] TMP_Text _content;
        [SerializeField] TMP_Text _data;
        [SerializeField] CanvasGroup _group;
        
        public void Bind(NewsItem newsItem)
        {
            _title.text = string.IsNullOrEmpty(newsItem.Title) ? "No title" : newsItem.Title;
            _content.text = string.IsNullOrEmpty(newsItem.Content) ? "No content" : newsItem.Content;
            _data.text = newsItem.Timestamp == _defaultDate ? "No date" : newsItem.Timestamp.ToShortDateString();
        }

        void Awake()
        {
            if (!_group) Debug.LogError($"{gameObject.name}: _group is null");
            if (!_title) Debug.LogError($"{gameObject.name}: _title is null");
            if (!_content) Debug.LogError($"{gameObject.name}: _content is null");
            if (!_data) Debug.LogError($"{gameObject.name}: _data is null");
        }

        IEnumerator Start()
        {
            _group.alpha = 0;
            var timer = _showUpTime;
            while (timer > 0)
            {
                _group.alpha = 1 - timer / _showUpTime;
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}