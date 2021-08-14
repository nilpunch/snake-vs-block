using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Snake.Domain
{
    public class Snake : INumbered
    {
        public Snake(int startNumber)
        {
            if (startNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(startNumber));

            Number = startNumber;
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
                throw new InvalidOperationException("Snake is dead");
            
            Number += amount;
            
            NumberChanged?.Invoke();
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            if (Dead)
                throw new InvalidOperationException("Snake is dead");
                
            Number = Mathf.Max(0, Number - amount);
            
            NumberChanged?.Invoke();
            
            if (Dead)
                Died?.Invoke();
        }
    }
}