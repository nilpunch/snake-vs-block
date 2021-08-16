using System;
using SnakeVsBlock.Domain;
using UnityEngine;

namespace SnakeVsBlock
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private float _distanceBetweenRows = 10f;
        
        private BlocksFactory _blocksFactory;
        private ITarget _target;

        private float _currentGenerationHeight;

        public void Init(BlocksFactory blocksFactory, ITarget target)
        {
            _target = target;
            _blocksFactory = blocksFactory;
            
            _target.PositionChanged += OnTargetPositionChanged;

            _currentGenerationHeight = _target.Position.y;
        }

        private void OnTargetPositionChanged()
        {
            if (_target.Position.y > _currentGenerationHeight)
            {
                _currentGenerationHeight += _distanceBetweenRows;
                _blocksFactory.GenerateBlocks(5, _currentGenerationHeight);
            }
        }
    }
}