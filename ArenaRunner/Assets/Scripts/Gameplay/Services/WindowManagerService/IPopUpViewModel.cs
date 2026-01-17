namespace Gameplay.Services
{
    public interface IPopUpViewModel
    {
        WindowName Name { get; }
        void Close();
    }
}