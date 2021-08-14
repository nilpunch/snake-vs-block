using System;

namespace Snake.Domain
{
    public interface IReadonlyNumbered
    {
        event Action NumberChanged;
        event Action Died;
        
        int Number { get; }
        bool Dead { get; }
    }
}