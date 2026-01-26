using System;
using System.Collections.Generic;
using UnityEngine;

namespace _TopDownShooter.Scripts.Utils.StateMachine
{
    public class FSM<T> where T : Enum 
    {
        static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;
        Dictionary<T, FSMState<T>> _states = new();
        bool _isInitialized;
        bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isInitialized = _isActive == value ? _isInitialized : false;
                if (CurrentState != null && _isActive && !value)
                {
                    CurrentState.OnExit();
                }
                _isActive = value;
            }
        }
        public FSMState<T> CurrentState {get; private set;}
        public void Tick(float deltaTime)
        {
            if (CurrentState == null) Debug.LogError("FSM: CurrentState is null");
            if (!IsActive) return;
            if (!_isInitialized)
            {
                _isInitialized = true;
                CurrentState.OnEnter();
            }
            
            CurrentState.Tick(deltaTime);
            
            CheckTransitions();
        }
        public void CheckTransitions()
        {
            var nextStateKey = CurrentState.GetNextState();
            if (!Comparer.Equals(nextStateKey, CurrentState.StateName))
            {
                Transition(nextStateKey);
            }
        }
        public void Transition(T stateName)
        {
            var newState = _states[stateName];
            if (newState == null)
            {
                Debug.LogError($"State with name {stateName} doesn't exist");
            }
            CurrentState.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
        public FSM<T> AddState(FSMState<T> state)
        {
            if (CurrentState == null)
            {
                CurrentState = state;
            }
            _states.Add(state.StateName, state);
            return this;
        }
    }
}