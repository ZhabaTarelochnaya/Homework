using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Application;

namespace _RSS.Scripts.Data
{
    public static class NewsLoader
    {
        public static async void SaveNewsAsync(NewsData newsData, string path, CancellationToken ct)
        {
            try
            {
                if (newsData ==  null) Debug.LogError("SaveNews: news is null");
                var json = JsonUtility.ToJson(newsData, true);
                await File.WriteAllTextAsync(streamingAssetsPath + path + ".json", json, ct);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public static async Task<List<NewsItemData>> LoadNewsAsync(string path, CancellationToken ct)
        {
            string json = "";
            try
            {
                json = await File.ReadAllTextAsync(streamingAssetsPath + path + ".json", ct);
                var news = JsonUtility.FromJson<NewsData>(json);
                return news.Items;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }
}