using UnityEngine;

namespace HelicopterTag.Gameplay.Camera
{
    public class HelicopterCamera : MonoBehaviour
    {
        [Header("Target")] [SerializeField] private Transform _target;

        [Header("Position")] [SerializeField] private Vector3 _moveOffset =
            new Vector3(0f, 6f, -10f);

        [SerializeField] private float _moveSmoothTime = 0.15f;

        [Header("Vertical Follow")] [SerializeField]
        private float _verticalFollowSpeed = 4f;

        [SerializeField] private float _hoverIgnoreDistance = 0.08f;

        [Header("Rotation")] [SerializeField] private Vector3 _rotateOffset;

        [SerializeField] private float _rotateSmoothSpeed = 8f;

        private Vector3 _positionVelocity;

        private float _cameraHeight;

        private void Start()
        {
            _cameraHeight = transform.position.y;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            _positionVelocity = Vector3.zero;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            FollowPosition();
            FollowRotation();
        }

        private void FollowPosition()
        {
            Vector3 targetPosition = _target.TransformPoint(_moveOffset);
            Vector3 currentPosition = transform.position;
            Vector3 horizontalTarget = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);

            transform.position = Vector3.SmoothDamp(transform.position, horizontalTarget, 
                ref _positionVelocity, _moveSmoothTime);


            float targetHeight = targetPosition.y;
            float heightDifference = targetHeight - _cameraHeight;

            if (Mathf.Abs(heightDifference) > _hoverIgnoreDistance)
            {
                _cameraHeight = Mathf.Lerp(_cameraHeight, targetHeight, _verticalFollowSpeed * Time.deltaTime);
            }

            Vector3 finalPosition = transform.position;
            finalPosition.y = _cameraHeight;
            transform.position = finalPosition;
        }

        private void FollowRotation()
        {
            Vector3 lookTarget = new Vector3(_target.position.x, transform.position.y, _target.position.z) +
                                 _rotateOffset;

            Vector3 direction = lookTarget - transform.position;

            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                _rotateSmoothSpeed * Time.deltaTime);
        }
    }
}