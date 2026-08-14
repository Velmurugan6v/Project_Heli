using HelicopterTag.Gameplay.Match;
using TMPro;
using UnityEngine;

namespace HelicopterTag.UI.Gameplay
{
    public class MatchResultEntryView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private TMP_Text _resultText;

        public void Initialize(PlayerResult result, MatchResultFormatter formatter)
        {
            _rankText.text = $"{result.Rank}";
            _playerNameText.text = $"{result.Player.name}";
            _resultText.text = formatter.Format(result);
        }
    }
}