using System;
using UnityEngine;

namespace Snake.Domain
{
    public class Block : INumbered
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

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            if (Dead)
                throw new InvalidOperationException("Block is dead");
            
            Number += amount;
            
            NumberChanged?.Invoke();
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            if (Dead)
                throw new InvalidOperationException("Block is dead");

            int lastNumber = Number;
            Number = Mathf.Max(0, Number - amount);
            
            _score.AddScore(lastNumber - Number);
            
            NumberChanged?.Invoke();
            
            if (Dead)
                Died?.Invoke();
        }
    }
}