using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace SnakeVsBlock.Domain
{
    public class BlockNumbersGenerator
    {
        private readonly IReadonlyNumbered _snake;
        private readonly int _maxNumber;

        public BlockNumbersGenerator(IReadonlyNumbered snake, int maxNumber)
        {
            _snake = snake;
            _maxNumber = maxNumber;
        }

        public IEnumerable<int> GenerateNumbers(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
                
            if (count == 0)
                yield break;
            
            int healthDependentNumber = Random.Range(1, _snake.Number);

            if (_snake.Number == 1)
                healthDependentNumber = 0;

            int specialNumberIndex = Random.Range(0, count);

            for (int i = 0; i < count; ++i)
            {
                if (i == specialNumberIndex)
                {
                    yield return healthDependentNumber;
                    continue;
                }
                
                int generatedNumber = Random.Range(1, _maxNumber + 1);
                yield return generatedNumber;
            }
        }
    }
}