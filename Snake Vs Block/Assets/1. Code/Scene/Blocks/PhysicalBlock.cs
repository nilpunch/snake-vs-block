using System;
using UnityEngine;

namespace SnakeVsBlock
{
    public class PhysicalBlock : MonoBehaviour
    {
        [SerializeField] private BlocksSettings _blocksSettings = null;
        
        public event Action Collided;

        private bool _isInCollision;
        private float _tickTime;
        private float _tickDelayFactor;
        
        private void Update()
        {
            if (_isInCollision == false)
                return;

            _tickDelayFactor += Time.deltaTime / _blocksSettings.TimeForSpeedUpTicks;
            _tickDelayFactor = Mathf.Clamp01(_tickDelayFactor);
            _tickTime -= Time.deltaTime;

            if (_tickTime <= 0f)
            {
                _tickTime = Mathf.Lerp(_blocksSettings.MaximumTickDelay, _blocksSettings.MinimumTickDelay,
                    _tickDelayFactor);
                Collided?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody.TryGetComponent<SnakeHead>(out _))
            {
                _isInCollision = true;
                _tickDelayFactor = 0f;
                _tickTime = 0f;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.attachedRigidbody.TryGetComponent<SnakeHead>(out _))
            {
                _isInCollision = false;
            }
        }
    }
}