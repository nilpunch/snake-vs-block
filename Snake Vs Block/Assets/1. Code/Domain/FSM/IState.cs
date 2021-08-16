namespace SnakeVsBlock.Domain
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}