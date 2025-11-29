using System;

namespace _1.Gameplay
{
    public class GameplayRequests
    {
        public Action LoadMainMenuRequested;
        public GameplayRequests(Action loadMainMenuRequested)
        {
            LoadMainMenuRequested += loadMainMenuRequested;
        }
    }
}