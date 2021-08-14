using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Path
    {
        private readonly Queue<Vector2Reference> _points;
        private readonly int _pointsCapacity;

        private Vector2Reference _firstEntered;

        public Path(int pointsCapacity)
        {
            if (pointsCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(pointsCapacity));

            _pointsCapacity = pointsCapacity;
            _points = new Queue<Vector2Reference>(pointsCapacity);
        }

        public IReadOnlyCollection<Vector2Reference> Points => _points;

        public Vector2 First => _firstEntered.Vector;

        public void AddPoint(Vector2 point)
        {
            if (Points.Count == _pointsCapacity)
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