using System;
using UniRx;
using UnityEngine;

namespace Snake
{
    public interface IMovingInput
    {
        event Action<Vector2> Delta;
        event Action<Vector2> Absolute;
    }
}