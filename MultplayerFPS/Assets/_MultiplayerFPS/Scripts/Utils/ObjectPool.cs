using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    T prefab;
    Transform parent;
    Queue<T> pool = new Queue<T>();
    public int Count => pool.Count;
    public ObjectPool(T prefab, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
    }
    
    T CreateNewObject()
    {
        T obj = GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }
        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }
    /// <summary>
    /// Returns true if object was instantiated.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>isInstantiated</returns>
    public bool Get(out T obj)
    {
        bool isPoolEmpty = pool.Count == 0;
        if (isPoolEmpty)
        {
            CreateNewObject();
        }
        obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return isPoolEmpty;
    }
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void Clear()
    {
        while (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            if (obj)
            {
                Object.Destroy(obj.gameObject);
            }
        }
    }
}