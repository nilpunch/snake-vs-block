using UnityEngine;

namespace Snake
{
    public class CollisionSolver : MonoBehaviour
    {
        [SerializeField] private bool _sendTriggerMessage = false;
        [SerializeField] private LayerMask _layerMask = -1;
        [SerializeField] private float _skinWidth = 0.1f;
 
        private float _minimumExtent; 
        private float _partialExtent; 
        private float _sqrMinimumExtent; 
        private Vector3 _previousPosition; 
        private Transform _transform;
        private Collider _collider;
 
        private void Start() 
        { 
            _transform = transform;
            _collider = GetComponent<Collider>();
            _previousPosition = _transform.position; 
            _minimumExtent = Mathf.Min(Mathf.Min(_collider.bounds.extents.x, _collider.bounds.extents.y), _collider.bounds.extents.z); 
            _partialExtent = _minimumExtent * (1.0f - _skinWidth); 
            _sqrMinimumExtent = _minimumExtent * _minimumExtent; 
        }

        public void FixedUpdate()
        { 
            //have we moved more than our minimum extent? 
            Vector3 movementThisStep = _transform.position - _previousPosition; 
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;
 
            if (movementSqrMagnitude > _sqrMinimumExtent) 
            { 
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
                RaycastHit hitInfo; 
 
                //check for obstructions we might have missed 
                if (Physics.Raycast(_previousPosition, movementThisStep, out hitInfo, movementMagnitude, _layerMask.value))
                {
                    if (!hitInfo.collider)
                        return;
 
                    if (hitInfo.collider.isTrigger) 
                        hitInfo.collider.SendMessage("OnTriggerEnter", _collider);
 
                    if (!hitInfo.collider.isTrigger)
                        _transform.position = hitInfo.point - (movementThisStep / movementMagnitude) * _partialExtent; 
 
                }
            } 
 
            _previousPosition = _transform.position; 
        }
    }
}