using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SnakeVsBlock
{
    public class SnakeFragmentsArranger : MonoBehaviour
    {
        [SerializeField] private SnakeSettings _snakeSettings = null;
        [SerializeField] private SnakeFragment _snakeFragmentPrefab = null;
        [SerializeField] private float _removingTime = 0.05f;

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
        }

        private void OnDestroy()
        {
            _path.Updated -= OnPathUpdated;
        }

        public void SetSnakeLenght(int snakeLenght)
        {
            if (snakeLenght < 0)
                throw new ArgumentOutOfRangeException(nameof(snakeLenght));
            
            if (_snakeFragmentsInUse.Count < snakeLenght)
                while (_snakeFragmentsInUse.Count != snakeLenght)
                    AddFragment();
            
            if (_snakeFragmentsInUse.Count > snakeLenght)
                while (_snakeFragmentsInUse.Count != snakeLenght)
                    RemoveFragment();
        }
        
        public void AddFragment()
        {
            _snakeFragmentsInUse.Enqueue(_snakeFragmentPool.Get());
        }

        public void RemoveFragment()
        {
            var removedFragment = _snakeFragmentsInUse.Dequeue();
            removedFragment.PlayDeathFx();
            _snakeFragmentPool.Return(removedFragment);

            _pathOffset += _snakeSettings.SnakeHeadRadius * 2f;

            if (_pathOffsetTween != null)
                _pathOffsetTween.Kill();
            
            _pathOffsetTween = DOTween.To(() => _pathOffset, value => _pathOffset = value, 0f,
                _removingTime);
        }

        private void OnPathUpdated()
        {
            if (_path.Count == 0 || _snakeFragmentsInUse.Count == 0)
                return;
            
            var positionOnPath = _path
                .Evaluate(_snakeFragmentsInUse.Count, _snakeSettings.SnakeHeadRadius, _pathOffset).GetEnumerator();
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