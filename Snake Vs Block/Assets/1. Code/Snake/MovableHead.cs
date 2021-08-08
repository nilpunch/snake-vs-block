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

        private float _accumulatedOffset;
        private Rigidbody2D _rigidbody;
        
        Vector3 ITarget.Position => transform.position;

        public void Init(IMovingInput movingInput)
        {
            if (movingInput == null)
                throw new ArgumentNullException(nameof(movingInput));
            
            movingInput.Delta
                .Subscribe(OnDeltaMove)
                .AddTo(this);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            transform.position += Vector3.right * (_accumulatedOffset * Time.deltaTime * _speedX);
            _accumulatedOffset -= _accumulatedOffset * Time.deltaTime * _speedX;
            transform.position += Vector3.up * (_speedY * Time.deltaTime);
        }

        private void OnDeltaMove(Vector2 delta)
        {
            _accumulatedOffset += delta.x;
        }
    }
}