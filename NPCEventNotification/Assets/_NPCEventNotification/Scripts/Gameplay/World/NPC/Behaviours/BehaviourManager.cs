using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NPCEventNotification.Scripts.Gameplay.NPC.Behaviours
{
    public class BehaviourManager
    {
        Dictionary<BehaviourName, Behaviour> _behaviours = new();
        bool _isInitialized = false;
        public Behaviour CurrentBehaviour { get; private set; }
        public void Tick(float deltaTime)
        {
            if (CurrentBehaviour == null) Debug.LogError("BehaviourManager have no behaviour set");
            if (!_isInitialized)
            {
                _isInitialized = true;
                CurrentBehaviour.OnEnter();
            }
            CurrentBehaviour.Tick(deltaTime);
        }
        public void SwitchBehaviour(BehaviourName behaviourName)
        {
            if (CurrentBehaviour.Name == behaviourName) return;
            var behaviour = _behaviours[behaviourName];
            if (behaviour == null)
            {
                Debug.LogError($"State with name {behaviourName} doesn't exist");
            }
            CurrentBehaviour.OnExit();
            CurrentBehaviour = behaviour;
            CurrentBehaviour.OnEnter();
        }
        public BehaviourManager Add(Behaviour behaviour)
        {
            if (CurrentBehaviour == null)
            {
                CurrentBehaviour = behaviour;
            }
            _behaviours.Add(behaviour.Name, behaviour);
            return this;
        }
    }
}