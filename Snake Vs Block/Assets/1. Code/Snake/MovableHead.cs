using System;
using Extensions;
using JetBrains.Annotations;
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

        Vector3 ITarget.Position => transform.position;
        public event Action PositionChanged;

        public void Init(IMovingInput movingInput)
        {
            if (movingInput == null)
                throw new ArgumentNullException(nameof(movingInput));
            
            movingInput.Delta.Subscribe(OnDeltaInput).AddTo(this);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            PositionChanged?.Invoke();

            Vector2 movement = Vector2.right * (_accumulatedXOffset * _speedX) + Vector2.up * _speedY;
            _rigidbody.MovePosition(_rigidbody.position + movement * Time.deltaTime);
            
            _accumulatedXOffset -= _accumulatedXOffset * Time.deltaTime * _speedX;
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            _accumulatedXOffset = 0f;
        }

        private void OnDeltaInput(Vector2 delta)
        {
            _accumulatedXOffset += delta.x;
        }
    }
}