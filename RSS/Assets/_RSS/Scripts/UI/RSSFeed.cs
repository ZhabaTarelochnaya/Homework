using System;
using System.Collections;
using System.Collections.Generic;
using _RSS.Scripts.Data;
using _RSS.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class RSSFeed : MonoBehaviour
{
    [SerializeField] GameObject NewsItemViewPrefab;
    [SerializeField] GameObject LoadingItem;
    News _news;
    ScrollRect _scrollRect;

    public void Bind(News news)
    {
        _news = news;
    }
    void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        if (!_scrollRect) Debug.LogError("RSSFeed: _scrollRect is not set");
    }

    void Start()
    {
        StartCoroutine(ShowNewsCoroutine());
    }

    public IEnumerator ShowNewsCoroutine()
    {
        LoadingItem.SetActive(true);
        while (_news == null) yield return null;
        foreach (var newsItem in _news.NewsItems)
        {
            yield return new WaitForSeconds(1f);
            var instance = Instantiate(NewsItemViewPrefab, _scrollRect.content);
            var newsItemView = instance.GetComponent<NewsItemView>();
            newsItemView.Bind(newsItem);
        }
        LoadingItem.SetActive(false);
    }
}
