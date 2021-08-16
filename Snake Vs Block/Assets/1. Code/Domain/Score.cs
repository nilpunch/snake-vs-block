using System;

namespace SnakeVsBlock.Domain
{
    public class Score
    {
        public Score()
        {
            Value = 0;
        }

        public event Action Changed;

        public int Value { get; private set; }

        public void AddScore(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Value += amount;
            
            Changed?.Invoke();
        }
    }
}