using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TopDownShooter.Scripts.Utils
{
    public class ObjectPool<T> where T : Component
    {
        readonly Func<T> _factory;
        Queue<T> _pool = new ();
        
        public ObjectPool(Func<T> factory)
        {
            _factory = factory;
        }
        
        public void Push(T item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
        
        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _factory();
            item.gameObject.SetActive(true);
            return item;
        }
    }
}