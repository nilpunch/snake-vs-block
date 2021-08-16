using UnityEngine;

namespace SnakeVsBlock
{
    public class Vector2Reference
    {
        public Vector2 Vector;

        public static implicit operator Vector2(Vector2Reference vector2Reference)
        {
            return vector2Reference.Vector;
        }
    }
}