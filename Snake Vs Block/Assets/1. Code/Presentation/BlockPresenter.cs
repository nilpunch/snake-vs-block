using System;
using SnakeVsBlock.Domain;

namespace SnakeVsBlock
{
    public class BlockPresenter : IDisposable
    {
        private readonly INumbered _snake;
        private readonly Block _blockModel;
        private readonly BlockContext _blockView;

        public BlockPresenter(INumbered snake, Block blockModel, BlockContext blockView)
        {
            _snake = snake;
            _blockModel = blockModel ?? throw new ArgumentNullException(nameof(blockModel));
            _blockView = blockView ?? throw new ArgumentNullException(nameof(blockView));
            
            _blockView.Visual.SetNumberText(blockModel.Number);
            
            _blockView.Physical.Collided += OnBlockCollided;
            _blockModel.NumberChanged += OnNumberChanged;
            _blockModel.Died += OnBlockDied;
        }

        public void Dispose()
        {
            _blockView.Physical.Collided -= OnBlockCollided;
            _blockModel.NumberChanged -= OnNumberChanged;
            _blockModel.Died -= OnBlockDied;
        }

        private void OnBlockCollided()
        {
            _blockModel.DecreaseForOther(1, _snake);
        }

        private void OnNumberChanged()
        {
            _blockView.Visual.SetNumberText(_blockModel.Number);
            _blockView.Visual.PlayScalingAnimation();
        }

        private void OnBlockDied()
        {
            _blockView.Visual.PlayDeathAnimation();
        }
    }
}