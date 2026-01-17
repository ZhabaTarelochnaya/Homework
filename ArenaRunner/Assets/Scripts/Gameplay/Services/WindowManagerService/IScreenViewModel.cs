namespace Gameplay.Services
{
    public interface IScreenViewModel
    {
        WindowName Name { get; }
        void Close();
    }
}