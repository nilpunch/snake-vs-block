using System;

namespace SnakeVsBlock.Domain
{
    public interface IReadonlyNumbered
    {
        event Action NumberChanged;
        event Action Died;
        
        int Number { get; }
        bool Dead { get; }
    }
}