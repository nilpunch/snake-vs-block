using UnityEngine;

namespace SnakeVsBlock
{
    [CreateAssetMenu]
    public class SnakeSettings : ScriptableObject
    {
        [SerializeField] private float _snakeHeadRadius = 0.2f;
        [SerializeField] private float _snakeVerticalSpeed = 5f;
        [SerializeField] private float _snakeHorizontalSpeed = 20f;

        public float SnakeHeadRadius => _snakeHeadRadius;

        public float SnakeVerticalSpeed => _snakeVerticalSpeed;

        public float SnakeHorizontalSpeed => _snakeHorizontalSpeed;
    }
}