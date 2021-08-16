using System;
using UnityEngine;

namespace SnakeVsBlock.Domain
{
    public class Block : IReadonlyNumbered
    {
        private readonly Score _score;

        public Block(int number, Score score)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            Number = number;
            _score = score ?? throw new ArgumentNullException(nameof(score));
        }

        public event Action Died;

        public event Action NumberChanged;

        public bool Dead => Number == 0;
        
        public int Number { get; private set; }
        
        public void DecreaseForOther(int amount, INumbered numbered)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            if (Dead)
                throw new InvalidOperationException("Block is dead");

            int lastNumber = Number;
            Number = Mathf.Max(0, Number - amount);

            int delta = lastNumber - Number;
            _score.AddScore(lastNumber - Number);
            if (numbered.Dead == false)
                numbered.Decrease(delta);
            
            NumberChanged?.Invoke();
            
            if (Dead)
                Died?.Invoke();
        }
    }
}