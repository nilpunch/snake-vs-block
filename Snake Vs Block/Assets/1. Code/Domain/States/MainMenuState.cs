using Snake.Boot;

namespace Snake.Domain
{
    public class MainMenuState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly MainMenuContext _mainMenuContext;

        public MainMenuState(IStateMachine stateMachine, MainMenuContext mainMenuContext)
        {
            _stateMachine = stateMachine;
            _mainMenuContext = mainMenuContext;
        }
        
        void IState.OnEnter()
        {
            _mainMenuContext.SessionStarted += OnSessionStarted;
        }

        void IState.OnExit()
        {
            _mainMenuContext.SessionStarted -= OnSessionStarted;
        }

        private void OnSessionStarted()
        {
            _stateMachine.Enter<SessionState>();
        }
    }
}