using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Snake
{
    public class PointerInput : MonoBehaviour, IMovingInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private Vector2 _delta;
        private Vector2 _absolute;
        
        private Camera _raycastCamera;

        public event Action<Vector2> Delta;
        public event Action<Vector2> Absolute;

        public void Init(Camera raycastCamera)
        {
            _raycastCamera = raycastCamera;
            
            _delta = Vector2.zero;
            _absolute = Vector2.zero;

        }
        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _absolute = _raycastCamera.ScreenToWorldPoint(eventData.position);
            Absolute?.Invoke(_absolute);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Vector2 lastPosition = _absolute;
            _absolute = _raycastCamera.ScreenToWorldPoint(eventData.position);
            _delta = _absolute - lastPosition;
            
            Absolute?.Invoke(_absolute);
            Delta?.Invoke(_delta);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _delta = Vector2.zero;
        }
    }
}