using HelicopterTag.Core.Input;
using UnityEngine;

namespace HelicopterTag.Helicopter
{
    [RequireComponent(typeof(HelicopterInput))]
    [RequireComponent(typeof(HelicopterMotor))]
    public class HelicopterController : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private HelicopterMotor _motor;

        private void Awake()
        {
            _inputProvider = GetComponent<IInputProvider>();
            _motor = GetComponent<HelicopterMotor>();
        }

        private void FixedUpdate()
        {
            _motor.SetInput(_inputProvider.GetInputData());
        }
    }
}