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
        public bool IsLifting => _inputData.Lift != 0;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;

            Debug.Assert(_movementSettings != null,
                $"{gameObject.name} : {nameof(_movementSettings)} is not assigned!!");
        }

        public void SetInput(InputData inputData)
        {
            _inputData = inputData;
        }

        public void FixedUpdate()
        {
            UpdateForwardMovement();
            ApplyForwardDamping();

            UpdateLift();
            UpdateRotation();
            
            //StopForwardMovementWhenTurning();
            AlignForwardVelocity();

            AltituteLimit();
            LimitForwardSpeed();
            LimitLiftSpeed();
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
            if (_inputData.Move.y != 0)
                return;

            Vector3 velocity = _rigidbody.linearVelocity;
            Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

            if (horizontalVelocity.sqrMagnitude < 0.01f)
            {
                horizontalVelocity = Vector3.zero;
            }
            else
            {
                horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero,
                    _movementSettings.ForwardDamping * Time.fixedDeltaTime);
            }

            _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);
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

            float turnSpeedRadians = _currentTurnSpeed * Mathf.Deg2Rad;
            _rigidbody.angularVelocity = _transform.up * turnSpeedRadians;
        }


        private void AltituteLimit()
        {
            if (_inputData.Lift != 0)
                return;

            Vector3 velocity = _rigidbody.linearVelocity;

            // Slowly stop vertical movement.
            float newVerticalVelocity = Mathf.Lerp(
                velocity.y,
                0f,
                _movementSettings.HoverDamping *
                Time.fixedDeltaTime);

            // Tiny natural hover movement.
            float hoverMotion =
                Mathf.Sin(Time.time * _movementSettings.HoverSpeed)
                * _movementSettings.HoverAmount;

            newVerticalVelocity += hoverMotion;

            _rigidbody.linearVelocity = new Vector3(
                velocity.x,
                newVerticalVelocity,
                velocity.z);
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
        
        private void AlignForwardVelocity()
        {
            Vector3 velocity = _rigidbody.linearVelocity;

            Vector3 horizontalVelocity =
                new Vector3(
                    velocity.x,
                    0f,
                    velocity.z);

            if (horizontalVelocity.sqrMagnitude < 0.01f)
                return;

            float speed = horizontalVelocity.magnitude;

            Vector3 targetVelocity =
                _transform.forward * speed;

            Vector3 newVelocity =
                Vector3.Lerp(
                    horizontalVelocity,
                    targetVelocity,
                    _movementSettings.TurnVelocityAlignment *
                    Time.fixedDeltaTime);

            _rigidbody.linearVelocity =
                new Vector3(
                    newVelocity.x,
                    velocity.y,
                    newVelocity.z);
        }

        public void SetNetworkInput(Vector2 move, float lift)
        {
            InputData input = new InputData()
            {
                Move = move,
                Lift = lift
            };

            SetInput(input);
        }
    }
}