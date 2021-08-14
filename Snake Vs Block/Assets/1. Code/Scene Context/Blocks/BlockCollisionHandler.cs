using System;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(VisualBlock))]
    public class BlockCollisionHandler : MonoBehaviour
    {
        public event Action Collided;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<SnakeHead>(out _))
            {
                Collided?.Invoke();
            }
        }
    }
    
    public class SnakeHead : MonoBehaviour { }
}