using System;
using Extensions;
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
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 movingAmount = (_target.Position + _followOffset - transform.position) / _smoothness * Time.deltaTime;

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