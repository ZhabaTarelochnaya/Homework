using System;

namespace _MultiplayerFPS.Scripts.Utils.FiniteStateMachine
{
    public abstract class FSMState<T> where T : Enum
    {
        public T StateName { get; private set; }
        public FSMState(T stateName)
        {
            StateName = stateName;
        }
        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void Tick(float deltaTime) {}
        public abstract T GetNextState();
    }
}