using System;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Tag.Events;
using UnityEngine;

namespace HelicopterTag.Gameplay.Tag
{
    public class HelicopterTagVisual : MonoBehaviour
    {
        [SerializeField] private TagParticipant _participant;
        [SerializeField] private GameObject _itIndicator;

        [SerializeField] private GameObject _protectionIndicator;

        private void Awake()
        {
            _itIndicator.SetActive(false);
            _protectionIndicator.SetActive(false);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<ItChangedEvent>(OnItChanged);
            EventBus.Subscribe<TagProtectionChangedEvent>(OnProtectionChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ItChangedEvent>(OnItChanged);
            EventBus.Unsubscribe<TagProtectionChangedEvent>(OnProtectionChanged);
        }

        private void OnItChanged(ItChangedEvent eventData)
        {
            bool isThisHelicopterIt = eventData.CurrentIt.Equals(_participant);
            _itIndicator.SetActive(isThisHelicopterIt);
        }

        private void OnProtectionChanged(TagProtectionChangedEvent eventData)
        {
            if (eventData.Participant != _participant) return;
            _protectionIndicator.SetActive(eventData.IsProtected);
        }
    }
}