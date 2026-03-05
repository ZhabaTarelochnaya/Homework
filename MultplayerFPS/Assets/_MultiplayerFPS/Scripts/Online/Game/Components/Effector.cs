using System;
using System.Collections.Generic;
using _MultiplayerFPS.Scripts.Config.Effector;
using _MultiplayerFPS.Scripts.Services.State;
using _MultiplayerFPS.Scripts.State;
using _MultiplayerFPS.Scripts.Utils.ServiceLocator;
using Mirror;
using UnityEngine;

namespace _MultiplayerFPS.Scripts.Components
{
    public class Effector : NetworkBehaviour
    {
        Dictionary<uint, Coroutine[]> _playerNetIdToEffects = new ();
        [SerializeField] Effect[] _effects;
        [ServerCallback]
        void OnTriggerEnter(Collider other)
        {
            var playerState = other.GetComponentInParent<PlayerState>();
            Coroutine[] coroutines = new Coroutine[_effects.Length];
            for (int i = 0; i < _effects.Length; i++)
            {
                coroutines[i] = StartCoroutine(_effects[i].Execute(playerState));
            }
            _playerNetIdToEffects.Add(playerState.netId, coroutines);
            playerState.IsDeadChanged += PlayerStateOnIsDeadChanged;
        }

        void PlayerStateOnIsDeadChanged(PlayerState playerState, bool obj)
        {
            if (isServer)
            {
                RemoveEffects(playerState.netId);
                playerState.IsDeadChanged -= PlayerStateOnIsDeadChanged;
            }
        }
        [ServerCallback]
        void OnTriggerExit(Collider other)
        {
            var playerState = other.GetComponentInParent<PlayerState>();
            RemoveEffects(playerState.netId);
            playerState.IsDeadChanged -= PlayerStateOnIsDeadChanged;
        }
        void RemoveEffects(uint netId)
        {
            if (!_playerNetIdToEffects.TryGetValue(netId, out var coroutines))
                return;
            for (int i = 0; i < _effects.Length; i++)
            {
                StopCoroutine(coroutines[i]);
            }
            _playerNetIdToEffects.Remove(netId);
        }
    }
}