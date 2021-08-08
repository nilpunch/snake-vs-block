using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snake
{
    public class PathCirclesArranger
    {
        private readonly Path _path;
        private readonly float _circleRadius;

        public PathCirclesArranger(int segmentsAmount, float circleRadius)
        {
            _circleRadius = circleRadius;
            _path = new Path(segmentsAmount);
        }

        public int Count => _path.Points.Count;

        public void AddPoint(Vector2 point)
        {
            _path.AddPoint(point);
        }

        public void UpdateFirst(Vector2 position)
        {
            _path.UpdateFirst(position);
        }

        public IEnumerable<Vector2> Evaluate(int circlesAmount)
        {
            if (circlesAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(circlesAmount));

            if (_path.Points.Count == 0)
                throw new InvalidOperationException();

            if (_path.Points.Count == 1)
            {
                yield return _path.Points.Last();
                yield break;
            }

            var reversedPath = _path.Points.Reverse().GetEnumerator();
            reversedPath.MoveNext();
            
            Vector2 lastCirclePosition = reversedPath.Current;
            yield return lastCirclePosition;

            int circlesEvaluated = 1;
            Vector2 lastPoint = lastCirclePosition;

            while (circlesEvaluated != circlesAmount)
            {
                Vector2 point = reversedPath.Current;
                
                float distanceFromLastCircle = Vector2.Distance(point, lastCirclePosition);

                if (distanceFromLastCircle < _circleRadius * 2f)
                {
                    lastPoint = point;
                    if (reversedPath.MoveNext() == false)
                        break;
                    continue;
                }

                Vector2 newCirclePosition = CalculateOtherCircleCenter(lastPoint, point, lastCirclePosition);

                yield return newCirclePosition;
                circlesEvaluated += 1;
                lastCirclePosition = newCirclePosition;
            }
            
            while (circlesEvaluated != circlesAmount)
            {
                yield return lastPoint;
                circlesEvaluated += 1;
            }
        }

        private Vector2 CalculateOtherCircleCenter(Vector2 start, Vector2 end, Vector2 circleCenter)
        {
            Vector2 direction = end - start;
            Vector2 directionToTangent = Vector2.Dot(circleCenter - start, direction)
                / direction.sqrMagnitude * direction;
            Vector2 tangentPoint = start + directionToTangent;
            float distanceToLine = ((circleCenter - start) - directionToTangent).magnitude;
            float hypotenuse = _circleRadius * 2f;

            float otherTriangleLength = Mathf.Sqrt(hypotenuse * hypotenuse - distanceToLine * distanceToLine);

            return tangentPoint + direction.normalized * otherTriangleLength;
        }
    }
}