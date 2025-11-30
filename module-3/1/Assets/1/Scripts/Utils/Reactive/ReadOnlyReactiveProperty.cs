using System;
using System.Collections.Generic;

namespace _1.Scripts.Utils
{
    public class ReadOnlyReactiveProperty <T>
    {
        T _value;
        readonly HashSet<Action<T>> _subscribers = new ();

        public ReadOnlyReactiveProperty(T initialCurrentValue = default)
        {
            CurrentValue = initialCurrentValue;
        }

        public T CurrentValue
        {
            get => _value;
            protected set
            {
                _value = value;
                NotifySubscribers();
            }
        }
        public IDisposable Subscribe(Action<T> onValueChanged)
        {
            _subscribers.Add(onValueChanged);
            onValueChanged?.Invoke(CurrentValue);
        
            return new Subscription(() => _subscribers.Remove(onValueChanged));
        }

        void NotifySubscribers()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber?.Invoke(CurrentValue);
            }
        }
    }
}