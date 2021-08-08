using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Vector2Reference
    {
        public Vector2 Vector;

        public static implicit operator Vector2(Vector2Reference vector2Reference)
        {
            return vector2Reference.Vector;
        }
    }
    
    public class Path
    {
        private readonly Queue<Vector2Reference> _points;
        private readonly int _segmentsAmount;

        private Vector2Reference _firstEntered;

        public Path(int segmentsAmount)
        {
            if (segmentsAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(segmentsAmount));

            _segmentsAmount = segmentsAmount;
            _points = new Queue<Vector2Reference>(segmentsAmount);
        }

        public IReadOnlyCollection<Vector2Reference> Points => _points;

        public void AddPoint(Vector2 point)
        {
            if (Points.Count == _segmentsAmount)
            {
                Vector2Reference lastPoint = _points.Dequeue();
                _firstEntered = lastPoint;
                _firstEntered.Vector = point;
            }
            else
            {
                _firstEntered = new Vector2Reference {Vector = point};
            }

            _points.Enqueue(_firstEntered);
        }

        public void UpdateFirst(Vector2 point)
        {
            _firstEntered.Vector = point;
        }
    }
}