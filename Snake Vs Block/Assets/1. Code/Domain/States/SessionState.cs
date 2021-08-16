using SnakeVsBlock.Boot;

namespace SnakeVsBlock.Domain
{
    public class SessionState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly SessionContext _sessionContext;

        public SessionState(IStateMachine stateMachine, SessionContext sessionContext)
        {
            _stateMachine = stateMachine;
            _sessionContext = sessionContext;
        }
        
        void IState.OnEnter()
        {
            _sessionContext.SessionEnded += OnSessionEnded;
        }

        void IState.OnExit()
        {
            _sessionContext.SessionEnded -= OnSessionEnded;
        }

        private void OnSessionEnded()
        {
            _stateMachine.Enter<ResultState>();
        }
    }
}