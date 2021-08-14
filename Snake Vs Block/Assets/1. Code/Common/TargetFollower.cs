using System;
using UnityEngine;

namespace Snake
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothness = 10f;
        [SerializeField] private bool _freezeX = true;
        [SerializeField] private bool _freezeY = true;
        [SerializeField] private bool _freezeZ = true;
        [SerializeField] private Vector3 _followOffset = Vector3.zero;
        
        private ITarget _target;

        public void Init(ITarget target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _target.PositionChanged += OnPositionChanged;
        }

        private void OnDestroy()
        {
            _target.PositionChanged -= OnPositionChanged;
        }

        public void OnPositionChanged()
        {
            Vector3 movingAmount = (_target.Position + _followOffset - transform.position) * Mathf.LerpUnclamped(1f, Time.deltaTime, _smoothness);

            if (_freezeX)
                movingAmount.x = 0f;
            if (_freezeY)
                movingAmount.y = 0f;
            if (_freezeZ)
                movingAmount.z = 0f;
            
            transform.position += movingAmount;
        }
    }
}