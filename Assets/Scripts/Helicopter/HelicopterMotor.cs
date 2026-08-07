using HelicopterTag.Core.Input;
using HelicopterTag.Helicopter.Config;
using UnityEngine;

namespace HelicopterTag.Helicopter
{
    [RequireComponent(typeof(Rigidbody))]
    public class HelicopterMotor : MonoBehaviour
    {
        private Transform _transform;
        private InputData _inputData;
        private Rigidbody _rigidbody;
        [SerializeField] private HelicopterMovementSettings _movementSettings;


        private float _targetHoverHeight;
        private float _currentTurnSpeed;

        public float CurrentTurnSpeed => _currentTurnSpeed;
        public float CurrentForwardSpeed => Vector3.Dot(_rigidbody.linearVelocity, transform.forward);
        public HelicopterMovementSettings MovementSettings => _movementSettings;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            Debug.Assert(_movementSettings != null,
                $"{gameObject.name} : {nameof(_movementSettings)} is not assigned!!");

            _targetHoverHeight = _transform.position.y;
        }

        public void SetInput(InputData inputData)
        {
            _inputData = inputData;
        }

        private void FixedUpdate()
        {
            UpdateForwardMovement();
            ApplyForwardDamping();
            UpdateLift();
            UpdateRotation();
            AltituteLimit();
            LimitForwardSpeed();
            LimitLiftSpeed();
            //AltitudeLimit();
        }


        private void UpdateForwardMovement()
        {
            float forwardInput = _inputData.Move.y;
            Vector3 forwardDirection = _transform.forward;
            Vector3 movementForce = forwardDirection * (forwardInput * _movementSettings.MoveForce);

            _rigidbody.AddForce(movementForce, ForceMode.Acceleration);
        }

        private void ApplyForwardDamping()
        {
            if (_inputData.Move.y != 0) return;

            float forwardSpeed = Vector3.Dot(_rigidbody.linearVelocity, _transform.forward);

            if (Mathf.Abs(forwardSpeed) < 0.1f)
                return;
            float brakingStregth = forwardSpeed * _movementSettings.ForwardDamping;
            Vector3 brakingForce = -_transform.forward * brakingStregth;
            _rigidbody.AddForce(brakingForce, ForceMode.Acceleration);
        }

        private void UpdateLift()
        {
            if (_inputData.Lift == 0)
                return;

            Vector3 liftForce = Vector3.up * (_inputData.Lift * _movementSettings.LiftForce);
            _rigidbody.AddForce(liftForce, ForceMode.Acceleration);
            _targetHoverHeight = _transform.position.y;
        }

        private void UpdateRotation()
        {
            float turnInput = _inputData.Move.x;
            float targetTurnSpeed = turnInput * _movementSettings.TurnSpeed;
            float maxTurnChange = _movementSettings.TurnAcceleration * Time.fixedDeltaTime;

            _currentTurnSpeed = Mathf.MoveTowards(_currentTurnSpeed, targetTurnSpeed, maxTurnChange);

            //Vector3 angularVelocity = new Vector3(0, _currentTurnSpeed, 0);
            //_rigidbody.angularVelocity = angularVelocity;
            _rigidbody.angularVelocity = _transform.up * (Mathf.Deg2Rad * _currentTurnSpeed);
        }


        private void AltituteLimit()
        {
            if (_inputData.Lift != 0)
                return;

            float currentHeight = _transform.position.y;
            float heightDifference = _targetHoverHeight - currentHeight;

            float hoverForce = heightDifference * _movementSettings.HoverStrength;

            Vector3 hoverForceVector = Vector3.up * hoverForce;

            _rigidbody.AddForce(hoverForceVector, ForceMode.Acceleration);
        }

        private void LimitLiftSpeed()
        {
            if (_inputData.Lift == 0)
                return;

            float liftSpeed = Vector3.Dot(_rigidbody.linearVelocity, _transform.up);

            //GameLogger.Log($"Lift speed: {liftSpeed}");

            if (Mathf.Abs(liftSpeed) <= _movementSettings.MaxLiftSpeed)
                return;

            Vector3 liftVelocity = _transform.up * liftSpeed;
            Vector3 remainingVelocity = _rigidbody.linearVelocity - liftVelocity;

            float clampedLiftSpeed =
                Mathf.Clamp(liftSpeed, -_movementSettings.MaxLiftSpeed, _movementSettings.MaxLiftSpeed);

            Vector3 clampedLiftVelocity = _transform.up * clampedLiftSpeed;

            _rigidbody.linearVelocity = clampedLiftVelocity + remainingVelocity;
        }

        private void LimitForwardSpeed()
        {
            float forwardSpeed = Vector3.Dot(_rigidbody.linearVelocity, _transform.forward);

            //GameLogger.Log($"Forward Speed: {forwardSpeed}");

            if (Mathf.Abs(forwardSpeed) <= _movementSettings.MaxForwardSpeed)
                return;

            Vector3 forwardVelocity = _transform.forward * forwardSpeed;
            Vector3 remainingVelocity = _rigidbody.linearVelocity - forwardVelocity;

            float clampedForwardSpeed = Mathf.Clamp(forwardSpeed, -_movementSettings.MaxForwardSpeed,
                _movementSettings.MaxForwardSpeed);

            Vector3 clampedForwardVelocity = _transform.forward * clampedForwardSpeed;

            _rigidbody.linearVelocity = clampedForwardVelocity + remainingVelocity;
        }


        private void AltitudeLimit()
        {
            if (_inputData.Lift == 0)
                return;

            float currentHeight = _transform.position.y;

            if (currentHeight <= _movementSettings.MaxHeight)
                return;

            float verticalSpeed = Vector3.Dot(_rigidbody.linearVelocity, _transform.up);

            if (verticalSpeed <= 0f)
                return;

            Vector3 verticalVelocity = _transform.up * verticalSpeed;
            Vector3 remainingVelocity = _rigidbody.linearVelocity - verticalVelocity;

            Vector3 clampedVerticalVelocity = Vector3.zero;

            _rigidbody.linearVelocity = remainingVelocity + clampedVerticalVelocity;
        }
    }
}