using System;
using HelicopterTag.Core.Events;
using HelicopterTag.Gameplay.Tag;
using UnityEngine;
using UnityEngine.UI;

namespace HelicopterTag.UI.Gameplay
{
    public class TagButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _tagButton;
        private ITagAction _tagAction;
        private ITagInputRequest _tagInput;

        private void Awake()
        {
            _tagButton.interactable = false;
            _tagButton.onClick.AddListener(OnTagButtonClicked);
        }

        private void OnEnable()
        {
            LocalPlayerRegistry.LocalTagActionChanged += OnTagActionChanged;
            LocalPlayerRegistry.LocalTagInputChanged += OnTagInputChanged;


            OnTagActionChanged(LocalPlayerRegistry.LocalTagAction);
            OnTagInputChanged(LocalPlayerRegistry.LocalTagInput);
        }

        private void OnDisable()
        {
            LocalPlayerRegistry.LocalTagActionChanged -= OnTagActionChanged;
            LocalPlayerRegistry.LocalTagInputChanged -= OnTagInputChanged;
        }

        private void OnTagActionChanged(ITagAction tagAction)
        {
            _tagAction = tagAction;

            if (_tagAction == null)
                _tagButton.interactable = false;
        }

        private void OnTagInputChanged(ITagInputRequest tagInput)
        {
            _tagInput = tagInput;
        }

        private void Update()
        {
            if (_tagAction == null)
            {
                _tagButton.interactable = false;
                return;
            }

            Debug.Log($"Can Tag: {_tagAction.CanTag}");
            _tagButton.interactable = _tagAction.CanTag;
        }

        public void OnTagButtonClicked()
        {
            if (_tagAction == null) return;

            if (!_tagAction.CanTag) return;

            if (_tagInput == null) return;

            _tagAction.TryTag();
        }
    }
}