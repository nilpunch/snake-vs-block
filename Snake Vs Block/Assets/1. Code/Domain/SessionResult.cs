using System;
using UnityEngine;

namespace Snake.Domain
{
    public readonly struct SessionResult
    {
        public readonly int Score;

        public SessionResult(int score)
        {
            if (score <= 0)
                throw new ArgumentOutOfRangeException(nameof(score));
            
            Score = score;
        }
    }
}