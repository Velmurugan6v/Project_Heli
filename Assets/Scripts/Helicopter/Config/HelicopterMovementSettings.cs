using UnityEngine;

namespace HelicopterTag.Helicopter.Config
{
    [CreateAssetMenu(fileName = "HelicopterSettings", menuName = "Helicopter Tag/Helicopter Settings")]
    public class HelicopterMovementSettings : ScriptableObject
    {
        [Header("Movement Force")] [SerializeField]
        private float moveForce;

        [SerializeField] private float liftForce;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float forwardDamping = 12f;


        [Header("Movement Limits")] [SerializeField]
        private float maxForwardSpeed;

        [SerializeField] private float maxLiftSpeed;
        [SerializeField] private float maxTurnSpeed;
        [SerializeField] private float turnAcceleration;
        [SerializeField] private float maxHeight;

        [Header("Hover Assist")] [SerializeField]
        private bool hoverAssistEnabled;

        [SerializeField] private float hoverStrength;
        [SerializeField] private float hoverForce;
        [SerializeField] private float hoverDeadZone;
        [SerializeField] private float _hoverHeight = 1.5f;
        [SerializeField] private float _verticalDamping = 3f;
        [SerializeField] private float _hoverDamping = 5f;
        [SerializeField] private float _hoverAmount = 0.05f;
        [SerializeField] private float _hoverSpeed = 2f;

        [Header("Physics")] [SerializeField] private float linearDamping;
        [SerializeField] private float angularDamping;
        [SerializeField] private float _turnVelocityAlignment = 3f;

        public float MoveForce => moveForce;
        public float LiftForce => liftForce;
        public float TurnSpeed => turnSpeed;
        public float ForwardDamping => forwardDamping;
        public float MaxForwardSpeed => maxForwardSpeed;
        public float MaxLiftSpeed => maxLiftSpeed;
        public float HoverForce => hoverForce;
        public float HoverDeadZone => hoverDeadZone;
        public float HoverStrength => hoverStrength;
        public bool HoverAssistEnabled => hoverAssistEnabled;
        public float MaxHeight => maxHeight;
        public float TurnAcceleration => turnAcceleration;
        public float TurnVelocityAlignment => _turnVelocityAlignment;
        public float HoverHeight => _hoverHeight;
        public float VerticalDamping => _verticalDamping;
        public float HoverDamping => _hoverDamping;


        public float HoverAmount => _hoverAmount;
        public float HoverSpeed => _hoverSpeed;
    }
}