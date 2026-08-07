using System;
using System.Collections;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Events;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class CountdownView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private float _goDisplayDuration = 1f;

        private Coroutine _hideCoroutine;


        private void OnEnable()
        {
            EventBus.Subscribe<CountdownChangedEvent>(OnCountdownChanged);
            EventBus.Subscribe<MatchStartedEvent>(OnMatchStarted);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<CountdownChangedEvent>(OnCountdownChanged);
            EventBus.Unsubscribe<MatchStartedEvent>(OnMatchStarted);

            if (_hideCoroutine == null) return;

            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        private void OnCountdownChanged(CountdownChangedEvent eventData)
        {
            _countdownText.gameObject.SetActive(true);
            _countdownText.text = eventData.Value.ToString();
        }

        private void OnMatchStarted(MatchStartedEvent eventData)
        {
            _countdownText.gameObject.SetActive(true);
            _countdownText.text = "Go!";

            if (_hideCoroutine != null) 
                StopCoroutine(_hideCoroutine);

            _hideCoroutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_goDisplayDuration);
            
            _countdownText.gameObject.SetActive(false);
            _hideCoroutine = null;
        }
    }
}