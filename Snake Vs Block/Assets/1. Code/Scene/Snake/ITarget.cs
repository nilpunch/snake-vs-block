using System;
using UnityEngine;

namespace SnakeVsBlock
{
    public interface ITarget
    {
        Vector3 Position { get; }
        event Action PositionChanged;
    }
}