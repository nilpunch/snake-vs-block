using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Snake
{
    public class MovingInput : MonoBehaviour, IMovingInput, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private ReactiveProperty<Vector2> _delta;
        private ReactiveProperty<Vector2> _absolute;
        private Camera _gameCamera;

        IReadOnlyReactiveProperty<Vector2> IMovingInput.Delta => _delta;
        IReadOnlyReactiveProperty<Vector2> IMovingInput.Absolute => _absolute;

        public void Init(Camera gameCamera)
        {
            _gameCamera = gameCamera;
        }

        private void Awake()
        {
            _delta = new ReactiveProperty<Vector2>(Vector2.zero);
            _absolute = new ReactiveProperty<Vector2>(Vector2.zero);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _absolute.Value = _gameCamera.ScreenToWorldPoint(eventData.position);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Vector2 lastPosition = _absolute.Value;
            _absolute.Value = _gameCamera.ScreenToWorldPoint(eventData.position);
            _delta.Value = _absolute.Value - lastPosition;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _delta.Value = Vector2.zero;
        }
    }
}