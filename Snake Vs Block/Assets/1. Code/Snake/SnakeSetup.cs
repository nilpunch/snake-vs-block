using System;
using UnityEngine;

namespace Snake
{
    public class SnakeSetup : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera = null;
        [SerializeField] private TargetFollower _cameraTargetFollower = null;
        [SerializeField] private MovingInput _movingInput = null;
        [SerializeField] private MovableHead _movableHead = null;
        [SerializeField] private SnakeFragments _snakeFragments = null;

        private void Awake()
        {
            _movingInput.Init(_gameCamera);
            _movableHead.Init(_movingInput);
            _snakeFragments.Init(_movableHead);
            _cameraTargetFollower.Init(_movableHead);
        }
    }
}