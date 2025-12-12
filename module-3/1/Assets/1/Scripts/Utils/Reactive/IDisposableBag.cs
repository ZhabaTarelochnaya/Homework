using System;
using System.Collections.Generic;

namespace _1.Scripts.Utils
{
    public class IDisposableBag : IDisposable
    {
        Stack<IDisposable> _disposables = new ();

        public void Add(IDisposable disposable) => _disposables.Push(disposable);

        public void Dispose()
        {
            while (_disposables.Count > 0)
            {
                _disposables.Pop().Dispose();
            }
        }
    }
}