using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Snake
{
    public class SnakeFragments : MonoBehaviour
    {
        [SerializeField] private SnakeFragment _snakeFragmentPrefab = null;
        [SerializeField] private int _pathCapacity = 1000;
        [SerializeField] private float _minimumDistanceForAdd = 0.2f;

        private Queue<SnakeFragment> _snakeFragmentsInUse;
        private SimplePool<SnakeFragment> _snakeFragmentPool;
        private ITarget _target;
        private CirclesPathArranger _path;

        private Vector2 _lastTargetPosition;

        public void Init(ITarget target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));

            _target.PositionChanged += OnPositionChanged;
            
            _snakeFragmentPool = new SimplePool<SnakeFragment>(_snakeFragmentPrefab, transform);
            _snakeFragmentsInUse = new Queue<SnakeFragment>();
            _path = new CirclesPathArranger(_pathCapacity, _snakeFragmentPrefab.Radius);
            AddFragment();
            AddFragment();
            AddFragment();
            AddFragment();
        }

        private void OnDestroy()
        {
            _target.PositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                AddFragment();
            
            if (_target == null)
                return;

            UpdatePath(_target.Position);

            if (_path.Count == 0 || _snakeFragmentsInUse.Count == 0)
                return;

            ArrangeFragmentsAlongPath();
        }

        public void AddFragment()
        {
            _snakeFragmentsInUse.Enqueue(_snakeFragmentPool.Get());
        }

        public void RemoveFragment()
        {
            _snakeFragmentPool.Return(_snakeFragmentsInUse.Dequeue());
        }

        private void UpdatePath(Vector2 targetPosition)
        {
            if (Vector3.Distance(_lastTargetPosition, targetPosition) > _minimumDistanceForAdd * _snakeFragmentPrefab.Radius)
            {
                _path.AddPoint(targetPosition);
                _lastTargetPosition = targetPosition;
            }
            else
            {
                _path.UpdateFirst(targetPosition);
            }
        }

        private void ArrangeFragmentsAlongPath()
        {
            var positionOnPath = _path.Evaluate(_snakeFragmentsInUse.Count).GetEnumerator();
            positionOnPath.MoveNext();

            foreach (var snakeFragment in _snakeFragmentsInUse)
            {
                snakeFragment.transform.position = positionOnPath.Current;
                positionOnPath.MoveNext();
            }

            positionOnPath.Dispose();
        }
    }
}