using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.Match
{
    public class MatchResetController : MonoBehaviour
    {
        public void ResetMatch()
        {
            EventBus.Publish(new MatchResetEvent());
        }
    }
}