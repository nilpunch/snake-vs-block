using System;
using UniRx;
using UnityEngine;

namespace Snake
{
    public class MovableHead : MonoBehaviour, ITarget
    {
        [SerializeField] private float _speedX = 5f;
        [SerializeField] private float _speedY = 5f;

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

            Vector2 movement = Vector2.right * (_accumulatedXOffset * _speedX) + Vector2.up * _speedY;

            Vector2 movePosition = _rigidbody.position + movement * Time.deltaTime;

            movePosition.x = Mathf.Clamp(movePosition.x, _horizontalBounds.Left, _horizontalBounds.Right);
            
            _rigidbody.MovePosition(movePosition);
            
            _accumulatedXOffset -= _accumulatedXOffset * Time.deltaTime * _speedX;
        }

        public void MoveX(Vector2 delta)
        {
            _accumulatedXOffset += delta.x;
        }
    }
}