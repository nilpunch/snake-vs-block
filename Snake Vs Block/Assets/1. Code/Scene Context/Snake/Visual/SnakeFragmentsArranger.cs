using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Snake
{
    [CreateAssetMenu]
    public class BalanceSettings : ScriptableObject
    {
        [SerializeField] private float _minimumNumberTickingDelay = 0.1f;
        [SerializeField] private float _maximumNumberTickingDelay = 0.4f;
        [SerializeField] private float _timeForTickingSpeedUp = 1f;

        public float MinimumNumberTickingDelay => _minimumNumberTickingDelay;

        public float MaximumNumberTickingDelay => _maximumNumberTickingDelay;

        public float TimeForTickingSpeedUp => _timeForTickingSpeedUp;
    }
    
    public class SnakeFragmentsArranger : MonoBehaviour
    {
        [SerializeField] private SnakeFragment _snakeFragmentPrefab = null;

        private Queue<SnakeFragment> _snakeFragmentsInUse;
        private Pool<SnakeFragment> _snakeFragmentPool;

        private ICirclesPath _path;
        private float _pathOffset;
        private Tween _pathOffsetTween;

        public void Init(ICirclesPath path)
        {
            _snakeFragmentPool = new Pool<SnakeFragment>(_snakeFragmentPrefab, transform);
            _snakeFragmentsInUse = new Queue<SnakeFragment>();
            _path = path;
            
            _path.Updated += OnPathUpdated;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                AddFragment();
            
            if (Input.GetKeyDown(KeyCode.X))
                RemoveFragment(0.5f);
        }

        private void OnDestroy()
        {
            _path.Updated -= OnPathUpdated;
        }

        public void AddFragment()
        {
            _snakeFragmentsInUse.Enqueue(_snakeFragmentPool.Get());
        }

        public void RemoveFragment(float delay)
        {
            _snakeFragmentPool.Return(_snakeFragmentsInUse.Dequeue());

            _pathOffset += _snakeFragmentPrefab.Radius * 2f;

            if (_pathOffsetTween != null)
                _pathOffsetTween.Kill();
            
            _pathOffsetTween = DOTween.To(() => _pathOffset, value => _pathOffset = value, 0f, delay);
        }

        private void OnPathUpdated()
        {
            if (_path.Count == 0 || _snakeFragmentsInUse.Count == 0)
                return;
            
            var positionOnPath = _path
                .Evaluate(_snakeFragmentsInUse.Count, _snakeFragmentPrefab.Radius, _pathOffset).GetEnumerator();
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