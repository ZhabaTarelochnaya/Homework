using System;
using System.Collections.Generic;

namespace _TopDownShooter.Scripts.Utils.Extensions
{
    public static class IListExtensions
    {
        public static T TakeRandom<T>(this IList<T> ts)
        {
            if (ts == null) throw new ArgumentNullException(nameof(ts));
            if (ts.Count == 0) throw new InvalidOperationException("Cannot take random element from an empty list");
            var index = UnityEngine.Random.Range(0, ts.Count);
            var randomElement = ts[index];
            ts.RemoveAt(index);
            return randomElement;
        }
    }
}