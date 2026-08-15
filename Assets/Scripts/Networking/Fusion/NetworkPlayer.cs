using Fusion;
using HelicopterTag.Core;
using UnityEngine;

namespace HelicopterTag.Networking.Fusion
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        public override void Spawned()
        {
            GameLogger.Log($"[NetworkPlayer] " + $"Object : {name}" + $"InputAuthority : {Object.InputAuthority}" +
                           $"`HasInputAuthority : {HasInputAuthority}");
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasInputAuthority)
                return;

            if (!GetInput<NetworkInputData>(out NetworkInputData inputData))
                return;

            Vector3 movement = new Vector3(inputData.Move.x, inputData.Move.y, 0);

            transform.position += movement * _moveSpeed * Runner.DeltaTime;
        }
    }
}