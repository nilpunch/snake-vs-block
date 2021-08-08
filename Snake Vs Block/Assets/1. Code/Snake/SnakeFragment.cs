using UnityEngine;

namespace Snake
{
    public class SnakeFragment : MonoBehaviour
    {
        [SerializeField] private Transform _sizeProvider = null;

        public float Radius => Mathf.Min(
            _sizeProvider.localScale.x,
            _sizeProvider.localScale.y, 
            _sizeProvider.localScale.z) / 2f;
    }
}