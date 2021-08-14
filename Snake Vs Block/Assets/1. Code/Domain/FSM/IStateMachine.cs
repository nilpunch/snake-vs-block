namespace Snake.Domain
{
    public interface IStateMachine
    {
        void Enter<T>() where T : IState;
    }
}