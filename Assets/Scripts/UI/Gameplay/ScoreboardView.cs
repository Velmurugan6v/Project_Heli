using System;
using System.Collections.Generic;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Player;
using HelicopterTag.Gameplay.Player.Event;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class ScoreboardView : MonoBehaviour
    {
        [SerializeField] private TagManager tagManager;
        [SerializeField] private Transform _content;
        [SerializeField] private ScoreEntryView _scoreEntryPrefab;

        private readonly Dictionary<PlayerContext, ScoreEntryView> _entries = new();


        private void Start()
        {
            Initialize(tagManager.GetPLayers());
        }

        public void Initialize(IReadOnlyList<PlayerContext> players)
        {
            foreach (PlayerContext player in players)
            {
                ScoreEntryView entry = Instantiate(_scoreEntryPrefab, _content);
                entry.Initialize(player);
                _entries.Add(player, entry);
            }
        }
    }
}