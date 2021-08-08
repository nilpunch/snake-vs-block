using UniRx;
using UnityEngine;

namespace Snake
{
    public interface IMovingInput
    {
        IReadOnlyReactiveProperty<Vector2> Delta { get; }
        IReadOnlyReactiveProperty<Vector2> Absolute { get; }
    }
}