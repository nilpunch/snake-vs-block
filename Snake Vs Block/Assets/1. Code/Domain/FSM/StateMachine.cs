using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Snake.Domain
{
    public class StateMachine : IStateMachine, IDisposable
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
            
            Register(NullState.Instance);
            Enter<NullState>();
        }

        public void Register<T>(T state) where T : IState
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            var stateType = typeof(T);
            if (_states.ContainsKey(stateType))
                throw new InvalidOperationException("State already registered");
            
            _states.Add(stateType, state);
        }

        public void Enter<T>() where T : IState
        {
            var stateType = typeof(T);
            if (_states.ContainsKey(stateType) == false)
                throw new InvalidOperationException("State not registered");
            
            _currentState.OnExit();
            _currentState = _states[stateType];
            _currentState.OnEnter();
        }
        
        public void Dispose()
        {
            _currentState.OnExit();
            _states.Clear();
        }
    }
}