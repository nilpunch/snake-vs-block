using System;
using SnakeVsBlock.Domain;
using UnityEngine;

namespace SnakeVsBlock
{
    public class SnakeSetup : MonoBehaviour
    {
        [SerializeField] private Camera _gameCamera = null;
        [SerializeField] private TargetFollower _cameraTargetFollower = null;
        [SerializeField] private PointerInput _pointerInput = null;
        [SerializeField] private MovableHead _movableHead = null;
        [SerializeField] private SnakePathUpdater _snakePathUpdater = null;
        [SerializeField] private SnakeFragmentsArranger _snakeFragmentsArranger = null;
        [SerializeField] private GameBounds _gameBounds = null;

        [Space, SerializeField] private LevelGenerator _levelGenerator = null;
        [SerializeField] private BlockContext _blockPrefab = null;
        
        private SnakePresenter _snakePresenter;
        private BlocksFactory _blocksFactory;

        private void Awake()
        {
            Snake snake = new Snake(100);
            Score score = new Score();

            _blocksFactory = new BlocksFactory(snake, score, _blockPrefab, _gameBounds,
                _levelGenerator.transform);
            
            CirclesPath circlesPath = new CirclesPath(1000);
            
            
            _pointerInput.Init(_gameCamera);
            _movableHead.Init(_pointerInput, _gameBounds);
            _cameraTargetFollower.Init(_movableHead);

            _snakePathUpdater.Init(_movableHead, circlesPath);
            _snakeFragmentsArranger.Init(circlesPath);
            
            _levelGenerator.Init(_blocksFactory, _movableHead);

            _snakePresenter = new SnakePresenter(snake, _snakeFragmentsArranger);
        }

        private void OnDestroy()
        {
            _snakePresenter.Dispose();
            _blocksFactory.Dispose();
        }
    }
}