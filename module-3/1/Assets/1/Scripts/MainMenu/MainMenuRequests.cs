using System;

namespace _1.MainMenu
{
    public class MainMenuRequests
    {
        public Action ExitRequested;
        public Action LoadGameplayRequested;
        public MainMenuRequests(Action exitRequested, Action loadGameplayRequested)
        {
            ExitRequested += exitRequested;
            LoadGameplayRequested += loadGameplayRequested;
        }
    }
}