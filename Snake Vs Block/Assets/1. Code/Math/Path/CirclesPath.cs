using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeVsBlock
{
    public interface ICirclesPath
    {
        event Action Updated;

        IEnumerable<Vector2> Evaluate(int circlesAmount, float circleRadius, float offset);
        
        int Count { get; }
    }
    
    public class CirclesPath : ICirclesPath
    {
        private readonly Path _path;

        public CirclesPath(int pointsCapacity)
        {
            _path = new Path(pointsCapacity);
        }

        public event Action Updated;
        
        public int Count => _path.Points.Count;

        public Vector2 First => _path.First;

        public IEnumerable<Vector2> Points => _path.Points.Select(vectorRef => vectorRef.Vector);


        public void AddPoint(Vector2 point)
        {
            _path.AddPoint(point);
            
            Updated?.Invoke();
        }

        public void UpdateFirst(Vector2 position)
        {
            if (_path.Points.Count == 0)
                _path.AddPoint(position);
            else
                _path.UpdateFirst(position);
            
            Updated?.Invoke();
        }

        public IEnumerable<Vector2> Evaluate(int circlesAmount, float circleRadius, float offset)
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
            float accumulatedLength = 0f;
            
            while (reversedPath.MoveNext())
            {
                Vector2 currentPosition = reversedPath.Current;
            
                float distanceFromLast = Vector2.Distance(currentPosition, lastCirclePosition);
                
                accumulatedLength += distanceFromLast;
            
                if (accumulatedLength > offset)
                {
                    lastCirclePosition = currentPosition -
                                         (currentPosition - lastCirclePosition).normalized *
                                         (accumulatedLength - offset);
                    break;
                }
            
                lastCirclePosition = currentPosition;
            }
            
            Vector2 lastPoint = lastCirclePosition;
            int circlesEvaluated = 0;

            yield return lastCirclePosition;

            while (circlesEvaluated != circlesAmount)
            {
                Vector2 point = reversedPath.Current;
                
                float distanceFromLastCircle = Vector2.Distance(point, lastCirclePosition);

                if (distanceFromLastCircle < circleRadius * 2f)
                {
                    lastPoint = point;

                    if (reversedPath.MoveNext() == false)
                        break;
                    continue;
                }


                Vector2 newCirclePosition = CalculateOtherCircleCenter(lastPoint, point, lastCirclePosition, 
                        circleRadius);
                
                yield return newCirclePosition;
                circlesEvaluated += 1;
                lastCirclePosition = newCirclePosition;
            }
            
            while (circlesEvaluated != circlesAmount)
            {
                yield return lastPoint;
                circlesEvaluated += 1;
            }
            
            reversedPath.Dispose();
        }

        private Vector2 CalculateOtherCircleCenter(Vector2 start, Vector2 end, Vector2 circleCenter, float radius,
            float offset = 0f)
        {
            Vector2 direction = end - start;

            if (direction.sqrMagnitude < 0.000001f)
                return end;
            
            Vector2 directionToTangent = Vector2.Dot(circleCenter - start, direction)
                / direction.sqrMagnitude * direction;
            Vector2 tangentPoint = start + directionToTangent;
            float distanceToLine = ((circleCenter - start) - directionToTangent).magnitude;
            float hypotenuse = radius * 2f;

            float thirdTriangleSide = Mathf.Sqrt(hypotenuse * hypotenuse - distanceToLine * distanceToLine);

            return tangentPoint + direction.normalized * (thirdTriangleSide + offset);
        }
    }
}