using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SnakeVsBlock
{
    public class VisualBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText = null;
        [SerializeField] private Transform _scaleRoot = null;
        [SerializeField] private Transform _visualScaleRoot = null;
        [SerializeField] private SpriteRenderer _sprite = null;

        [Header("Visual Settings")] 
        [SerializeField] private float _padding = 0.1f;
        
        [Space, SerializeField] private Color _colorWhen1 = Color.cyan;
        [Space, SerializeField] private Color _colorWhen50 = Color.red;
        
        [Space, SerializeField] private float _scalingMagnitude = 1.2f;
        [SerializeField] private float _scalingElasticity = 1f;
        [SerializeField] private float _scalingTime = 0.5f;
        
        [Space, SerializeField] private ParticleSystem _deathFx = null;
        
        private Tween _scalingTween = null;

        private void Awake()
        {
            _scalingTween = _visualScaleRoot
                .DOScale(Vector3.one * _scalingMagnitude, _scalingTime / 2f)
                .OnComplete(() => _scalingTween.Rewind())
                .SetAutoKill(false)
                .Pause();
        }

        private void OnDestroy()
        {
            _scalingTween?.Kill();
        }

        public void Prepare()
        {
            _visualScaleRoot.localScale = Vector3.one;
            _scaleRoot.gameObject.SetActive(true);
            _scalingTween?.Restart();
        }
        
        public void SetUnitSize(float unitSize)
        {
            _scaleRoot.localScale = Vector3.one * (unitSize - _padding);
        }

        public void SetNumberText(int number)
        {
            _numberText.text = number.ToString();
            
            Color.RGBToHSV(_colorWhen1, out float hueStart, out float sStart, out float vStart);
            Color.RGBToHSV(_colorWhen50, out float hueEnd, out float sEnd, out float vEnd);

            float factor = (float) number / 50;
            
            float resultHue = Mathf.Lerp(hueStart, hueEnd, factor);
            float resultSaturation = Mathf.Lerp(sStart, sEnd, factor);
            float resultValue = Mathf.Lerp(vStart, vEnd, factor);
            
            _sprite.color = Color.HSVToRGB(resultHue, resultSaturation, resultValue);
        }

        public void SetPosition(Vector2 position)
        {
            _scaleRoot.transform.position = position;
            _deathFx.transform.position = position;
        }

        public void PlayScalingAnimation()
        {
            if (_scalingTween != null && _scalingTween.IsPlaying())
                return;

            _scalingTween.Play();
        }

        public void PlayDeathAnimation()
        {
            _scaleRoot.gameObject.SetActive(false);
            if (_deathFx != null && _deathFx.isPlaying == false)
                _deathFx.Play();
        }
    }
}