using _Prototype.Scripts.Utils.ServiceLocator;

namespace _Prototype.Scripts.Services
{
    public interface ISceneLoaderService : IService
    {
        void LoadGameplay();
    }
}