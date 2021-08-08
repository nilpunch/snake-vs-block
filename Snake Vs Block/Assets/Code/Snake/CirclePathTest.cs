using System;
using Extensions;
using UnityEngine;

namespace Snake
{
    public class CirclePathTest : MonoBehaviour
    {
        [SerializeField] private int _pathSegments = 10;
        [SerializeField] private int _snakeCircles = 10;
        [SerializeField] private float _circlesRadius = 0.2f;
        [SerializeField] private float _updateDistance = 0.1f;

        private PathCirclesArranger _pathCirclesArranger;

        private Vector2 _lastHeadPosition;

        private void Awake()
        {
            _pathCirclesArranger = new PathCirclesArranger(_pathSegments, _circlesRadius);
        }

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            Plane raycastPlane = new Plane(Vector3.back, 0f);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (raycastPlane.Raycast(ray, out float distance))
            {
                Vector3 worldHitPoint = ray.GetPoint(distance);

                if (Vector3.Distance(_lastHeadPosition, worldHitPoint) > _updateDistance)
                {
                    _pathCirclesArranger.AddPoint(worldHitPoint);
                    _lastHeadPosition = worldHitPoint;
                }
                else
                {
                    _pathCirclesArranger.UpdateFirst(worldHitPoint);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            if (_pathCirclesArranger == null)
                return;

            if (_pathCirclesArranger.Count > 0)
            {
                foreach (var position in _pathCirclesArranger.Evaluate(_snakeCircles))
                {
                    Gizmos.DrawWireSphere(position, _circlesRadius);
                }
            }
        }
    }
}