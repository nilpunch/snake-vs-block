using System;
using UnityEngine;

namespace Snake
{
    public interface ITarget
    {
        Vector3 Position { get; }
        event Action PositionChanged;
    }
}