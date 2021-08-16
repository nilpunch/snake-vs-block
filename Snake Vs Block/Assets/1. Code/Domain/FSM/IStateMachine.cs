namespace SnakeVsBlock.Domain
{
    public interface IStateMachine
    {
        void Enter<T>() where T : IState;
    }
}