namespace SnakeVsBlock.Domain
{
    public interface INumbered : IReadonlyNumbered
    {
        void Decrease(int amount);
        void Increase(int amount);
    }
}