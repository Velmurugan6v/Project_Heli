using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HelicopterTag.Gameplay.Input
{
    public class MatchInput : MonoBehaviour, IMatchInput
    {
        [SerializeField] InputActionReference _startAction;

        public bool StartPressed => _startAction.action.WasPressedThisFrame();

        private void OnEnable()
        {
            _startAction.action.Enable();
        }

        private void OnDisable()
        {
            _startAction.action.Disable();
        }
    }
}
