using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Snake
{
    public class SimplePool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _root;
        private readonly Queue<T> _pool;
        
        public SimplePool(T prefab, Transform root, int prewarm = 10)
        {
            if (prewarm <= 0)
                throw new ArgumentOutOfRangeException(nameof(prewarm));
            
            _prefab = prefab;
            _root = root;
            _pool = new Queue<T>(prewarm);

            Prewarm(prewarm);
        }

        public T Get(Vector3? position = null)
        {
            T instance;
            if (_pool.Count != 0)
                instance = _pool.Dequeue();
            else
                instance = Object.Instantiate(_prefab, _root);
            
            instance.gameObject.SetActive(true);
            
            if (position.HasValue)
                instance.transform.position = position.Value;
            
            return instance;
        }

        public void Return(T instance)
        {
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }

        private void Prewarm(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                T instance = Object.Instantiate(_prefab, _root);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }
    }
}