namespace SnakeVsBlock.Domain
{
    public class NullState : IState
    {
        private NullState()
        {
        }

        public static readonly NullState Instance = new NullState();
        
        void IState.OnEnter()
        {
        }

        void IState.OnExit()
        {
        }
    }
}