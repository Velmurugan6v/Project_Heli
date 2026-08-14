using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace HelicopterTag.UI.Gameplay
{
    public class TagFeedEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _taggerText;
        [SerializeField] private TMP_Text _targetText;

        [SerializeField] private float _visibleDuration = 3f;
        [SerializeField] private float _fadeDuration = 0.5f;

        [SerializeField] private float _slideDistance = 300f;
        [SerializeField] private float _slideDuration = 0.3f;

        private RectTransform _rectTransform;
        private Vector2 _targetPosition;

        private CanvasGroup _canvasGroup;
        public Action<TagFeedEntryView> OnExpired;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup == null)
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            _targetPosition = _rectTransform.anchoredPosition;
        }

        public void Initialize(string taggerText, string targetText, TagFeedEntryType type)
        {
            _taggerText.text = taggerText;
            _targetText.text = targetText;

            ApplyType(type);
            PlayEntranceAnimation();
            PlayLifeTime();
        }

        private void ApplyType(TagFeedEntryType tagType)
        {
            switch (tagType)
            {
                case TagFeedEntryType.YouTagged:
                    _taggerText.text = "YOU";
                    break;

                case TagFeedEntryType.YouWereTagged:
                    _targetText.text = "YOU";
                    break;
            }
        }

        private void PlayEntranceAnimation()
        {
            _rectTransform.DOKill();
            _canvasGroup.DOKill();

            _rectTransform.anchoredPosition = _targetPosition + Vector2.left * _slideDistance;
            _canvasGroup.alpha = 0f;

            Sequence sequence = DOTween.Sequence();
            sequence.Join(_rectTransform.DOAnchorPos(_targetPosition, _slideDuration));
            sequence.Join(_canvasGroup.DOFade(1f, _slideDistance));
        }

        private void PlayLifeTime()
        {
            _canvasGroup.alpha = 1f;

            _canvasGroup.DOFade(0f, _fadeDuration).SetDelay(_visibleDuration)
                .OnComplete(() =>
                {
                    OnExpired?.Invoke(this);
                    Destroy(gameObject);
                });
        }

        private void FadeOut()
        {
            _canvasGroup.DOKill();

            _canvasGroup.DOFade(0f, _fadeDuration).OnComplete(Expire);
        }

        private void Expire()
        {
            OnExpired?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _rectTransform.DOKill();
            _canvasGroup.DOKill();

            OnExpired = null;
        }
    }
}