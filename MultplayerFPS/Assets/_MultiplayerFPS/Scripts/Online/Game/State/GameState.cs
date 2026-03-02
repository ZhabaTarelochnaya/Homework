using System;
using _MultiplayerFPS.Scripts.Components;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.State
{
    public class GameState : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnGameStateChanged)), HideInInspector]
        public GameStateName GameStateName;
        [SyncVar(hook = nameof(OnCurrentTimeChanged)), HideInInspector]
        public int CurrentTime;
        public readonly SyncDictionary<uint, PlayerState> PlayerStates = new ();
        public readonly SyncDictionary<uint, Pickup> ActivePickups = new();
        public readonly SyncDictionary<uint, PlayerScore> PlayerScores = new();
        
        public event Action<GameStateName> GameStateChanged;
        public event Action<int> CurrentTimeChanged;
        
        void OnGameStateChanged(GameStateName oldState, GameStateName newState) => GameStateChanged?.Invoke(newState);
        void OnCurrentTimeChanged(int oldTime, int newTime) => CurrentTimeChanged?.Invoke(newTime);
    }
}