using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class TagDetector : MonoBehaviour 
    {
        [SerializeField] private TagParticipant _participant;
        [SerializeField] private TagManager _tagManager;
        
        private void OnTriggerEnter(Collider other)
        {
            TagParticipant otherParticipant = other.GetComponent<TagParticipant>();
            
            if(otherParticipant==null)
                return;
            
            if(otherParticipant == _participant)
                return;
            
            if(!_participant.IsIt)
                return;
            
            _tagManager.TransferIt(otherParticipant);
        }
    }
}
