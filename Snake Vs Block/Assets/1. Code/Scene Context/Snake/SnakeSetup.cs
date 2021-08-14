using UnityEngine;

namespace Snake
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
        
        private void Awake()
        {
            CirclesPath circlesPath = new CirclesPath(1000);
            
            _pointerInput.Init(_gameCamera);
            _movableHead.Init(_pointerInput, _gameBounds);
            _cameraTargetFollower.Init(_movableHead);

            _snakePathUpdater.Init(_movableHead, circlesPath);
            _snakeFragmentsArranger.Init(circlesPath);
        }
    }
}