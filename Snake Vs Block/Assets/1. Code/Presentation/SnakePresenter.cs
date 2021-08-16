using System;
using SnakeVsBlock.Domain;

namespace SnakeVsBlock
{
    public class SnakePresenter : IDisposable
    {
        private readonly Snake _snakeModel;
        private readonly SnakeFragmentsArranger _snakeView;

        public SnakePresenter(Snake snakeModel, SnakeFragmentsArranger snakeView)
        {
            _snakeModel = snakeModel ?? throw new ArgumentNullException(nameof(snakeModel));
            _snakeView = snakeView ?? throw new ArgumentNullException(nameof(snakeView));
            
            _snakeView.SetSnakeLenght(_snakeModel.Number);
            
            _snakeModel.NumberChanged += OnNumberChanged;
        }

        public void Dispose()
        {
            _snakeModel.NumberChanged -= OnNumberChanged;
        }

        private void OnNumberChanged()
        {
            _snakeView.SetSnakeLenght(_snakeModel.Number);
        }
    }
}