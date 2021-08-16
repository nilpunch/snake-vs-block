using UnityEngine;

namespace SnakeVsBlock
{
    [CreateAssetMenu]
    public class BlocksSettings : ScriptableObject
    {
        [SerializeField] private float _minimumTickDelay = 0.1f;
        [SerializeField] private float _maximumTickDelay = 0.5f;
        [SerializeField] private float _timeForSpeedUpTicks = 1f;

        public float MinimumTickDelay => _minimumTickDelay;

        public float MaximumTickDelay => _maximumTickDelay;

        public float TimeForSpeedUpTicks => _timeForSpeedUpTicks;
    }
}