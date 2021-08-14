using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Snake.Domain;
using UnityEngine;

namespace Snake
{
    public class BlocksFactory
    {
        private readonly Score _score;
        private readonly IHorizontalBounds _horizontalBounds;
        private readonly Pool<VisualBlock> _blocksPool;
        private readonly BlockNumbersGenerator _numbersGenerator;

        private readonly List<IDisposable> _blockPresenters;

        public BlocksFactory(INumbered snake, Score score, VisualBlock prefab, IHorizontalBounds horizontalBounds)
        {
            _score = score;
            _horizontalBounds = horizontalBounds;
            _numbersGenerator = new BlockNumbersGenerator(snake, 50);
            _blocksPool = new Pool<VisualBlock>(prefab, null);
        }

        public void GenerateBlocks(int blocksAmount, float coordinateY)
        {
            float unitSize = _horizontalBounds.Width / blocksAmount;
            Vector2 newBlockPosition = new Vector2(_horizontalBounds.Left + unitSize / 2f, coordinateY);
            Vector2 positionIncrement = new Vector2(unitSize, 0f);
            
            foreach (var number in _numbersGenerator.GenerateNumbers(blocksAmount))
            {
                VisualBlock visualBlock = _blocksPool.Get();
                
                visualBlock.Prepare();
                visualBlock.SetUnitSize(unitSize);
                visualBlock.SetPosition(newBlockPosition);

                newBlockPosition += positionIncrement;
                
                Block block = new Block(number, _score);
                BlockPresenter blockPresenter = new BlockPresenter(block, visualBlock);

                _blockPresenters.Add(blockPresenter);
            }
        }
    }

    public class BlockPresenter : IDisposable
    {
        private readonly Block _blockModel;
        private readonly VisualBlock _blockView;

        public BlockPresenter(Block blockModel, VisualBlock blockView)
        {
            _blockModel = blockModel ?? throw new ArgumentNullException(nameof(blockModel));
            _blockView = blockView ?? throw new ArgumentNullException(nameof(blockView));
            
            
        }

        public void Dispose()
        {
            
        }
    }
}