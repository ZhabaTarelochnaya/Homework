using System;
using System.Collections.Generic;

namespace NPCEventNotification.Scripts.Utils.FiniteStateMachine
{
    public class FSM<T> where T : Enum 
    {
        FSMState<T> _currenState;
        Dictionary<T, FSMState<T>> _states = new();
        public bool IsActive {get; set;} = true;
        
        public void Tick(float deltaTime)
        {
            if (!IsActive) return;
            _currenState.Tick(deltaTime);
            var nextStateKey = _currenState.GetNextState();
            if (!nextStateKey.Equals(_currenState.StateName))
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
            _currenState.OnExit();
            _currenState = newState;
            _currenState.OnEnter();
        }
        public FSM<T> AddState(FSMState<T> state)
        {
            if (_currenState == null)
            {
                _currenState = state;
            }
            _states.Add(state.StateName, state);
            return this;
        }
    }
}