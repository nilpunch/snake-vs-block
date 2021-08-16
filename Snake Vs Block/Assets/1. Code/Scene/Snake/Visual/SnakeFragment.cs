using System;
using UnityEngine;

namespace SnakeVsBlock
{
    public class SnakeFragment : MonoBehaviour
    {
        [SerializeField] private SnakeSettings _snakeSettings = null;
        [SerializeField] private Transform _sizeProvider = null;
        [SerializeField] private ParticleSystem _deathFx = null;
        
        private void Awake()
        {
            _sizeProvider.localScale *= _snakeSettings.SnakeHeadRadius * 2f;
        }

        public void PlayDeathFx()
        {
            Instantiate(_deathFx, transform.position, Quaternion.identity, null).Play();
        }
    }
}