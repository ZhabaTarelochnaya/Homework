using System;
using System.Collections;
using System.Collections.Generic;
using _RSS.Scripts.Data;
using _RSS.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RSSFeed : MonoBehaviour
{
    bool _isLoadFast;
    List<NewsItem> _startNews;
    Coroutine _loadingRoutine;
    RSSFeedEvents _feedEvents;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] GameObject _newsItemViewPrefab;
    [SerializeField] GameObject _errorViewPrefab;
    [SerializeField] GameObject _loadingItem;
    [SerializeField] TMP_InputField _inputField;

    public void Bind(List<NewsItem> startNews, RSSFeedEvents feedEvents)
    {
        _startNews = startNews;
        _feedEvents = feedEvents;
        _feedEvents.NewsLoaded += FeedEventsOnNewsLoaded;
        _feedEvents.LoadFailed += FeedEventsOnLoadFailed;
    }
    void Awake()
    {
        if (!_scrollRect) Debug.LogError("RSSFeed: _scrollRect is not set");
        if  (!_loadingItem) Debug.LogError("RSSFeed: _newsItemViewPrefab is not set");
        if (!_newsItemViewPrefab) Debug.LogError("RSSFeed: _loadingItem is not set"); 
        if (!_errorViewPrefab) Debug.LogError("RSSFeed: _errorViewPrefab is not set");
        if (!_inputField) Debug.LogError("RSSFeed: _inputField is not set");
    }

    IEnumerator Start()
    {
        while (_startNews == null) yield return null;
        _loadingRoutine = StartCoroutine(ShowNewsCoroutine(_startNews));
    }
    
    public void OnReloadButtonPressed()
    {
        ResetFeed();
        _isLoadFast = false;
        _feedEvents.RequestLoadNews(_inputField.text);
    }
    public void OnFastReloadButtonPressed()
    {
        ResetFeed();
        _isLoadFast = true;
        _feedEvents.RequestLoadNews(_inputField.text);
    }
    
    void FeedEventsOnNewsLoaded(List<NewsItem> newsItems)
    {
        if (_isLoadFast)
        {
            ShowNews(newsItems);
        }
        else
        {
            _loadingRoutine = StartCoroutine(ShowNewsCoroutine(newsItems));
        }
    }
    void FeedEventsOnLoadFailed(Exception e)
    {
        var instance = Instantiate(_errorViewPrefab, _scrollRect.content);
        var errorView = instance.GetComponent<ErrorView>();
        errorView.Bind(e);
    }
    IEnumerator ShowNewsCoroutine(List<NewsItem> newsItems)
    {
        _loadingItem.SetActive(true);
        foreach (var newsItem in newsItems)
        {
            yield return new WaitForSeconds(1f);
            var instance = Instantiate(_newsItemViewPrefab, _scrollRect.content);
            var newsItemView = instance.GetComponent<NewsItemView>();
            newsItemView.Bind(newsItem);
        }
        _loadingItem.SetActive(false);
    }

    void ShowNews(List<NewsItem> newsItems)
    {
        foreach (var newsItem in newsItems)
        {
            var instance = Instantiate(_newsItemViewPrefab, _scrollRect.content);
            var newsItemView = instance.GetComponent<NewsItemView>();
            newsItemView.Bind(newsItem);
        }
    }

    void ClearFeed()
    {
        foreach (var newsItemView in _scrollRect.content.GetComponentsInChildren<NewsItemView>())
        {
            Destroy(newsItemView.gameObject);
        }

        foreach (var errorView in _scrollRect.content.GetComponentsInChildren<ErrorView>())
        {
            Destroy(errorView.gameObject);
        }
    }

    void ResetFeed()
    {
        StopCoroutine(_loadingRoutine);
        _loadingItem.SetActive(false);
        ClearFeed();
    }

    void OnDestroy()
    {
        _feedEvents.NewsLoaded -= FeedEventsOnNewsLoaded;
        _feedEvents.LoadFailed -= FeedEventsOnLoadFailed;
    }
}
