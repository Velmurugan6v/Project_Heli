using UnityEngine;
using HelicopterTag.Core.Input;

namespace HelicopterTag.Helicopter
{
    public class HelicopterInput : MonoBehaviour, IInputProvider
    {
        private InputData _inputData;
        public InputData InputData => _inputData;
        private HelicopterInput_Actions _inputActions;

        private void Awake()
        {
            _inputActions = new HelicopterInput_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()
        {
            _inputData.Move = _inputActions.Gameplay.Move.ReadValue<Vector2>();
            _inputData.Lift = _inputActions.Gameplay.Lift.ReadValue<float>();
            _inputData.Fire = _inputActions.Gameplay.Fire.IsPressed();

/*#if UNITY_EDITOR
            GameLogger.Log(
                $"MoveInput {_inputData.Move} : TurnInput {_inputData.Lift} : FirePressed {_inputData.Fire}");
#endif*/
        }

        public InputData GetInputData()
        {
            return _inputData;
        }
    }
}