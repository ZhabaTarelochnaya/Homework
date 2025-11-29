using System;

namespace _1.Gameplay
{
    public class GameplayRequests
    {
        public Action LoadMainMenuRequested;
        public Action ReloadGameplayRequested;
        public GameplayRequests(Action loadMainMenuRequested, Action reloadGameplayRequested)
        {
            LoadMainMenuRequested += loadMainMenuRequested;
            ReloadGameplayRequested += reloadGameplayRequested;
        }
    }
}