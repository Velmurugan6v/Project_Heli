using System;
using UnityEngine;

namespace HelicopterTag.Helicopter.Visuals
{
    public class HelicopterRotorAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _mainRotor;
        [SerializeField] private Transform _tailRotor;

        [SerializeField] private float mainRotorSpeed;
        [SerializeField] private float tailRotorSpeed;

        private void Update()
        {
            RotateMainRotor();
            RotateTailRotor();
        }

        private void RotateMainRotor()
        {
            _mainRotor.Rotate(Vector3.up * (mainRotorSpeed * Time.deltaTime), Space.Self);
        }

        private void RotateTailRotor()
        {
            _tailRotor.Rotate(Vector3.right * (tailRotorSpeed * Time.deltaTime), Space.Self);
        }
    }
}