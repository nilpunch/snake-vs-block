using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Snake
{
    public class VisualBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText = null;
        [SerializeField] private Transform _scaleRoot = null;
        [SerializeField] private Transform _visualScaleRoot = null;

        [Header("Visual Settings")] 
        [SerializeField] private float _padding = 0.1f;
        [SerializeField] private float _scalingMagnitude = 1.2f;
        [SerializeField] private float _scalingElasticity = 1f;
        [SerializeField] private float _scalingTime = 0.5f;
        [SerializeField] private ParticleSystem _deathFx = null;
        
        private Tween _scalingTween = null;

        public void Prepare()
        {
            _visualScaleRoot.localScale = Vector3.one;
            _scaleRoot.gameObject.SetActive(true);
            _scalingTween?.Kill();
        }
        
        public void SetUnitSize(float unitSize)
        {
            _scaleRoot.localScale = Vector3.one * (unitSize - _padding);
        }

        public void SetNumberText(int number)
        {
            _numberText.text = number.ToString();
        }

        public void SetPosition(Vector2 position)
        {
            _scaleRoot.transform.position = position;
        }

        public void PlayScalingAnimation()
        {
            if (_scalingTween != null && _scalingTween.IsPlaying())
                return;
            
            _scalingTween = _visualScaleRoot
                .DOPunchScale(Vector3.one * _scalingMagnitude, _scalingTime, elasticity: _scalingElasticity);
        }

        public void PlayDeathAnimation()
        {
            _scaleRoot.gameObject.SetActive(false);
            if (_deathFx.isPlaying == false)
                _deathFx.Play();
        }
    }
}