using UnityEngine;

namespace SnakeVsBlock
{
    public class GameBounds : MonoBehaviour, IHorizontalBounds
    {
        [SerializeField] private Transform _leftBorder = null;
        [SerializeField] private Transform _rightBorder = null;

        public float Width => _rightBorder.position.x - _leftBorder.position.x;

        public float Left => _leftBorder.position.x;

        public float Right => _rightBorder.position.x;
    }
}