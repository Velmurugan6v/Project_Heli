using UnityEngine;

namespace HelicopterTag.Helicopter.Visuals
{
    public class HelicopterModelPitcher : MonoBehaviour
    {
        [SerializeField] private Transform model;
        [SerializeField] private HelicopterMotor motor;

        [Header("Pitch Settings")]
        private float _currentPitchAngle;
        [SerializeField] private float maxPitchAngle;
        [SerializeField] private float pitchSmoothSpeed;

        private void LateUpdate()
        {
            HandlePitching();
        }

        private void HandlePitching()
        {
            float forwardSpeed = motor.CurrentForwardSpeed;
            float normalizedPitchAngle = Mathf.Clamp(forwardSpeed / motor.MovementSettings.MaxForwardSpeed, -1f, 1f);
            float targetPitchAngle = normalizedPitchAngle * maxPitchAngle;

            _currentPitchAngle = Mathf.Lerp(_currentPitchAngle, targetPitchAngle, pitchSmoothSpeed * Time.fixedDeltaTime);
            model.localRotation = Quaternion.Euler(_currentPitchAngle, 0, 0);
        }
    }
}