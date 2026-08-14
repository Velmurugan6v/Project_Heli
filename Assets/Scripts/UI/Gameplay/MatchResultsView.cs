using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Match.Events;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class MatchResultsView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Transform _content;
        [SerializeField] private MatchResultEntryView _entryPrefab;

        private MatchResultFormatter _formatter;

        private void Awake()
        {
            _formatter = new MatchResultFormatter();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<MatchResultReadyEvent>(OnMatchResultReady);
            EventBus.Subscribe<MatchResetEvent>(OnMatchReset);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchResultReadyEvent>(OnMatchResultReady);
            EventBus.Unsubscribe<MatchResetEvent>(OnMatchReset);
        }

        private void Start()
        {
            _panel.SetActive(false);
        }

        private void OnMatchResultReady(MatchResultReadyEvent eventData)
        {
            _panel.SetActive(true);
            ClearEntries();

            foreach (PlayerResult result in eventData.Result.Results)
            {
                MatchResultEntryView entry = Instantiate(_entryPrefab, _content);
                entry.Initialize(result, _formatter);
            }
        }

        private void OnMatchReset(MatchResetEvent eventData)
        {
            HideMatchResult();
        }

        private void ClearEntries()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }
        }

        private void HideMatchResult()
        {
            _panel?.SetActive(false);
        }
    }
}