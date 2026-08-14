using System.Collections.Generic;
using HelicopterTag.Gameplay.Player;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class ScoreboardView : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private Transform _content;
        [SerializeField] private ScoreEntryView _scoreEntryPrefab;

        private readonly Dictionary<PlayerContext, ScoreEntryView> _entries = new();


        private void Start()
        {
            Initialize(_playerManager.Players);
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