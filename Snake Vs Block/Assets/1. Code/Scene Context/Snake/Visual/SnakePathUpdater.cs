using System;
using UnityEngine;

namespace Snake
{
    public class SnakePathUpdater : MonoBehaviour
    {
        [SerializeField] private float _minimumDistanceForUpdate = 0.01f;

        private ITarget _target;
        private CirclesPath _path;

        private Vector2 _lastTargetPosition;

        public void Init(ITarget target, CirclesPath circlesPath)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));

            _target.PositionChanged += OnPositionChanged;
            
            _path = circlesPath;
        }

        private void OnDestroy()
        {
            _target.PositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            if (Vector3.Distance(_lastTargetPosition, (Vector2) _target.Position) > _minimumDistanceForUpdate)
            {
                _path.AddPoint(_target.Position);
                _lastTargetPosition = _target.Position;
                return;
            }

            _path.UpdateFirst(_target.Position);
        }
    }
}