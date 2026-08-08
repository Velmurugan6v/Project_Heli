using System;
using HelicopterTag.Core;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Config;
using HelicopterTag.Gameplay.Events;
using HelicopterTag.Gameplay.Match;
using HelicopterTag.Gameplay.Match.Events;
using HelicopterTag.Gameplay.Scoring.Strategies;
using UnityEngine;

namespace HelicopterTag.Gameplay.Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TagManager _tagManager;
        [SerializeField] private GameplayConfig config;
        private IScoreStrategy _scoreStrategy;

        private bool _isScoringActive;


        private void OnEnable()
        {
            EventBus.Subscribe<MatchStartedEvent>(OnMatchStarted);
            EventBus.Subscribe<MatchFinishedEvent>(OnMatchFinished);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MatchStartedEvent>(OnMatchStarted);
            EventBus.Unsubscribe<MatchFinishedEvent>(OnMatchFinished);
        }


        private void Start()
        {
            CreateStrategy();
            _scoreStrategy.Initialize();
            GameLogger.Log($"Score Strategy : {_scoreStrategy.GetType().Name}");
        }

        private void OnDestroy()
        {
            _scoreStrategy.Dispose();
        }

        private void Update()
        {
            if (!_isScoringActive)
                return;

            if (_scoreStrategy is ITickable tickable)
                tickable.Tick(Time.deltaTime);
        }

        private void OnMatchStarted(MatchStartedEvent eventData)
        {
            _isScoringActive = true;
        }

        private void OnMatchFinished(MatchFinishedEvent eventData)
        {
            _isScoringActive = false;

            MatchResult result = _scoreStrategy.GetMatchResult(_tagManager.GetPLayers());
            EventBus.Publish(new MatchResultReadyEvent(result));

            GameLogger.Log("MatchResultReadyEvent Published");
        }

        private void CreateStrategy()
        {
            switch (config.ScoringMode)
            {
                case ScoringMode.TagScore:
                    _scoreStrategy = new TagScoreStrategy();
                    break;

                case ScoringMode.SurvivalScore:
                    _scoreStrategy = new SurvivalTimeStrategy(_tagManager.Participants);
                    break;
            }
        }
    }
}