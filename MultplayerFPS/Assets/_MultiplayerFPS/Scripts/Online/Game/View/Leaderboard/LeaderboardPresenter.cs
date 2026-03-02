using System;
using System.Linq;
using _MultiplayerFPS.Scripts.Services.ServerCommands;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public class LeaderboardPresenter : IPresenter
    {
        readonly ILeaderboardView _view;
        readonly GameState _gameState;
        public LeaderboardPresenter(ILeaderboardView view)
        {
            _view = view;
            _gameState = ServiceLocator.Current.Get<IStateService>().GameState;
            _gameState.GameStateChanged += GameStateOnGameStateChanged;
        }
        void Refresh()
        {
            var data = _gameState.PlayerScores
                .Select(pair =>
                {
                    var netId = pair.Key;
                    var score = pair.Value;
                    var nickname = _gameState.PlayerStates[netId].Nickname;

                    return new LeaderboardData(nickname, score);
                })
                .OrderByDescending(d => d.Score)   
                .ThenBy(d => d.Deaths)            
                .ThenBy(d => d.Nickname)           
                .Take(8)                          
                .ToArray();
            
            Array.Resize(ref data, 8);
            _view.UpdateData(data);
        }
        void OnChange(SyncIDictionary<uint, PlayerScore>.Operation arg1, uint arg2, PlayerScore arg3)
        {
            Refresh();
        }
        public void Enable()
        {
            _view.Enable();
            _view.ReturnToLobbyButtonPressed += ViewOnReturnToLobbyButtonPressed;
            _gameState.PlayerScores.OnChange += OnChange;
            Refresh();
        }

        void GameStateOnGameStateChanged(GameStateName obj)
        {
            if (obj == GameStateName.MatchEnded && NetworkServer.active)
            {
                _view.SetReturnToLobbyButtonActive(true);
            }
        }

        public void Disable()
        {
            _view.Disable();
            _view.ReturnToLobbyButtonPressed -= ViewOnReturnToLobbyButtonPressed;
            _gameState.PlayerScores.OnChange -= OnChange;
        }
        void ViewOnReturnToLobbyButtonPressed()
        {
            Disable();
            ServiceLocator.Current.Get<IPlayerCommandsService>().ReturnToLobby();
        }
    }
}