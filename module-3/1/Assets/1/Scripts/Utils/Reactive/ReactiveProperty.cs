namespace _TestPlatformer.Scripts.Utils
{
    public class ReactiveProperty<T> : ReadOnlyReactiveProperty<T> where T : struct
    {
        public ReactiveProperty(T value =  default)
        {
            CurrentValue = value;
        }
        public T Value
        {
            get => CurrentValue;
            set => CurrentValue = value;
        }
    }
}