using System;
using System.Collections.Generic;

namespace NPCEventNotification.Scripts.Utils.FiniteStateMachine
{
    public class FSM<T> where T : Enum 
    {
        Dictionary<T, FSMState<T>> _states = new();
        bool _isInitialized;
        bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isInitialized = _isActive == value ? _isInitialized : false;
                _isActive = value;
            }
        }
        public FSMState<T> CurrentState {get; private set;}
        public void Tick(float deltaTime)
        {
            if (!IsActive) return;
            if (!_isInitialized)
            {
                _isInitialized = true;
                CurrentState.OnEnter();
            }
            CurrentState.Tick(deltaTime);
            var nextStateKey = CurrentState.GetNextState();
            if (!nextStateKey.Equals(CurrentState.StateName))
            {
                Transition(nextStateKey);
            }
        }
        public void Transition(T stateName)
        {
            var newState = _states[stateName];
            if (newState == null)
            {
                throw new Exception($"State with name {stateName} doesn't exist");
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