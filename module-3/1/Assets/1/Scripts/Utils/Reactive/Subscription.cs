using System;

namespace _TestPlatformer.Scripts.Utils
{
    public class Subscription : IDisposable
    {
        readonly Action _unsubscribeAction;
        bool _isDisposed;
        public Subscription(Action unsubscribeAction)
        {
            _unsubscribeAction = unsubscribeAction;
        }
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _unsubscribeAction?.Invoke();
                _isDisposed = true;
            }
        }
    }
}