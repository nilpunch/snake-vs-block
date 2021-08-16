using System;
using UniRx;
using UnityEngine;

namespace SnakeVsBlock
{
    public class MovableHead : MonoBehaviour, ITarget
    {
        [SerializeField] private SnakeSettings _snakeSettings = null;

        private float _accumulatedXOffset;
        private Rigidbody2D _rigidbody;
        private IHorizontalBounds _horizontalBounds;

        Vector3 ITarget.Position => transform.position;
        public event Action PositionChanged;

        public void Init(IMovingInput movingInput, IHorizontalBounds horizontalBounds)
        {
            if (movingInput == null)
                throw new ArgumentNullException(nameof(movingInput));

            _horizontalBounds = horizontalBounds;
            
            movingInput.Delta += MoveX;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            PositionChanged?.Invoke();

            Vector2 movement = Vector2.right * (_accumulatedXOffset * _snakeSettings.SnakeHorizontalSpeed) 
                               + Vector2.up * _snakeSettings.SnakeVerticalSpeed;

            Vector2 movePosition = _rigidbody.position + movement * Time.deltaTime;

            movePosition.x = Mathf.Clamp(movePosition.x,
                _horizontalBounds.Left + _snakeSettings.SnakeHeadRadius,
                _horizontalBounds.Right - _snakeSettings.SnakeHeadRadius);
            
            _rigidbody.MovePosition(movePosition);
            
            _accumulatedXOffset -= _accumulatedXOffset * Time.deltaTime * _snakeSettings.SnakeHorizontalSpeed;
        }

        public void MoveX(Vector2 delta)
        {
            _accumulatedXOffset += delta.x;
        }
    }
}