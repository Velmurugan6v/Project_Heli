using UnityEngine;

namespace HelicopterTag.Helicopter.Config
{
    [CreateAssetMenu(fileName = "HelicopterSettings", menuName = "Helicopter Tag/Helicopter Settings")]
    public class HelicopterMovementSettings : ScriptableObject
    {
        [Header("Movement Force")] 
        [SerializeField] private float moveForce;
        [SerializeField] private float liftForce;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float forwardDamping = 12f;


        [Header("Movement Limits")] 
        [SerializeField] private float maxForwardSpeed;
        [SerializeField] private float maxLiftSpeed;
        [SerializeField] private float maxTurnSpeed;
        [SerializeField] private float turnAcceleration;
        [SerializeField] private float maxHeight;

        [Header("Hover Assist")] 
        [SerializeField] private bool hoverAssistEnabled;
        [SerializeField] private float hoverStrength;
        [SerializeField] private float hoverForce;
        [SerializeField] private float hoverDeadZone;

        [Header("Physics")] 
        [SerializeField] private float linearDamping;
        [SerializeField] private float angularDamping;
        
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
    }
}