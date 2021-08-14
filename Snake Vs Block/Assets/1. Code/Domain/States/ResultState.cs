using Snake.Boot;

namespace Snake.Domain
{
    public class ResultState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ResultContext _resultContext;

        public ResultState(IStateMachine stateMachine, ResultContext resultContext)
        {
            _stateMachine = stateMachine;
            _resultContext = resultContext;
        }
        
        void IState.OnEnter()
        {
            _resultContext.Continued += OnContinued;
            _resultContext.Revived += OnRevived;
        }

        void IState.OnExit()
        {
            _resultContext.Continued += OnContinued;
            _resultContext.Revived += OnRevived;
        }

        private void OnContinued()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        private void OnRevived()
        {
            _stateMachine.Enter<SessionState>();
        }
    }
}