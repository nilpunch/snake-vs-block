namespace Snake.Domain
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
    }
}