using UnityEngine;

namespace HelicopterTag.Helicopter.Visuals
{
    public class HelicopterModelBanker : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private HelicopterMotor _motor;

        private float _currentBankAngle;
        [SerializeField] private float maxBankAngle = 20f;
        [SerializeField] private float bankSmoothSpeed = 10f;

        private void FixedUpdate()
        {
            HandleBank();
        }

        private void HandleBank()
        {
            float turnSpeed = _motor.CurrentTurnSpeed;
            float normalizedTurnSpeed = Mathf.Clamp(turnSpeed / _motor.MovementSettings.TurnSpeed, -1f, 1f);
            float targetBankAngle = normalizedTurnSpeed * maxBankAngle;


            _currentBankAngle = Mathf.Lerp(_currentBankAngle, targetBankAngle, bankSmoothSpeed * Time.fixedDeltaTime);

            _model.localRotation = Quaternion.Euler(0, 0, _currentBankAngle);
        }
    }
}