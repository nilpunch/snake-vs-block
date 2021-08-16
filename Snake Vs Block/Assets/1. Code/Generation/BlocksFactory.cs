using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SnakeVsBlock.Domain;
using UnityEngine;

namespace SnakeVsBlock
{
    public class BlocksFactory : IDisposable
    {
        private readonly INumbered _snake;
        private readonly Score _score;
        private readonly IHorizontalBounds _horizontalBounds;
        private readonly Pool<BlockContext> _blocksPool;
        private readonly BlockNumbersGenerator _numbersGenerator;

        private readonly List<IDisposable> _blockPresenters;

        public BlocksFactory(INumbered snake, Score score, BlockContext prefab, IHorizontalBounds horizontalBounds, Transform root)
        {
            _snake = snake;
            _score = score;
            _horizontalBounds = horizontalBounds;
            _numbersGenerator = new BlockNumbersGenerator(snake, 50);
            _blocksPool = new Pool<BlockContext>(prefab, root);

            _blockPresenters = new List<IDisposable>();
        }

        public void GenerateBlocks(int blocksAmount, float coordinateY)
        {
            float unitSize = _horizontalBounds.Width / blocksAmount;
            Vector2 newBlockPosition = new Vector2(_horizontalBounds.Left + unitSize / 2f, coordinateY);
            Vector2 positionIncrement = new Vector2(unitSize, 0f);
            
            foreach (var number in _numbersGenerator.GenerateNumbers(blocksAmount))
            {
                if (number == 0)
                {
                    newBlockPosition += positionIncrement;
                    continue;
                }
                
                BlockContext visualBlock = _blocksPool.Get();
                
                visualBlock.Visual.Prepare();
                visualBlock.Visual.SetUnitSize(unitSize);
                visualBlock.Visual.SetPosition(newBlockPosition);

                newBlockPosition += positionIncrement;
                
                Block block = new Block(number, _score);
                BlockPresenter blockPresenter = new BlockPresenter(_snake, block, visualBlock);
                _blockPresenters.Add(blockPresenter);
            }
        }
        
        public void Dispose()
        {
            _blockPresenters.ForEach(presenter => presenter.Dispose());
        }
    }
}