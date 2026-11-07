using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HelicopterTag.Gameplay.Input
{
    public class MatchInput : MonoBehaviour, IMatchInput
    {
        [SerializeField] InputActionReference _startAction;

        private bool _uiStartRequested;

        public bool StartPressed => _uiStartRequested;

        public void RequestStart()
        {
            _uiStartRequested = true;
        }

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