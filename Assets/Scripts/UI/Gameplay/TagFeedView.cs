using System;
using System.Collections.Generic;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Tag.Events;
using UnityEditor.Search;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class TagFeedView : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TagFeedEntryView _entryPrefab;

        [SerializeField] private PlayerContext _localPlayer;

        [SerializeField] private int _maxVisibleEntries = 3;

        private readonly List<TagFeedEntryView> _entries = new();

        //for testing
        [SerializeField] private PlayerContext playerOne, playerTwo;


        [ContextMenu("Add Tag Feed")]
        public void GenerateTagFeed()
        {
            PlayerTaggedEvent playerTaggedEvent = new PlayerTaggedEvent(playerOne, playerTwo);
            OnPlayerTagged(playerTaggedEvent);
        }

        public void OnEnable()
        {
            EventBus.Subscribe<PlayerTaggedEvent>(OnPlayerTagged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<PlayerTaggedEvent>(OnPlayerTagged);
        }

        private void OnPlayerTagged(PlayerTaggedEvent eventData)
        {
            TagFeedEntryType type = GetEntryType(eventData);
            TagFeedEntryView entry = Instantiate(_entryPrefab, _content);
            entry.Initialize(eventData.Tagger.DisplayName, eventData.Target.DisplayName, type);

            entry.OnExpired += RemoveEntry;
            _entries.Add(entry);
            RemoveOldEntries();
        }

        private TagFeedEntryType GetEntryType(PlayerTaggedEvent eventData)
        {
            if (eventData.Tagger == _localPlayer)
                return TagFeedEntryType.YouTagged;

            if (eventData.Target == _localPlayer)
                return TagFeedEntryType.YouWereTagged;

            return TagFeedEntryType.Normal;
        }

        private void RemoveEntry(TagFeedEntryView entry)
        {
            _entries.Remove(entry);
        }

        private void RemoveOldEntries()
        {
            while (_entries.Count > _maxVisibleEntries)
            {
                TagFeedEntryView oldEntry = _entries[0];
                _entries.RemoveAt(0);

                if (oldEntry != null)
                    Destroy(oldEntry.gameObject);
            }
        }
    }
}