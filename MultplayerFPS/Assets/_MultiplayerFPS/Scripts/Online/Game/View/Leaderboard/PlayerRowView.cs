using _MultiplayerFPS.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _MultiplayerFPS.Scripts.Leaderboard
{
    public class PlayerRowView : MonoBehaviour, IView
    {
        [SerializeField] Image _image;
        [SerializeField] TMP_Text _numberText;
        [SerializeField] TMP_Text _nicknameText;
        [SerializeField] TMP_Text _killsText;
        [SerializeField] TMP_Text _deathsText;
        [SerializeField] TMP_Text _scoreText;
        
        public void UpdateData(int number, LeaderboardData leaderboardData)
        {
            _numberText.text = number.ToString();
            _nicknameText.text = leaderboardData.Nickname;
            _killsText.text = leaderboardData.Kills.ToString();
            _deathsText.text = leaderboardData.Deaths.ToString();
            _scoreText.text = leaderboardData.Score.ToString();
        }

        public void Enable()
        {
            _image.enabled = true;
            _numberText.enabled = true;
            _nicknameText.enabled = true;
            _killsText.enabled = true;
            _deathsText.enabled = true;
            _scoreText.enabled = true;
        }

        public void Disable()
        {
            _image.enabled = false;
            _numberText.enabled = false;
            _nicknameText.enabled = false;
            _killsText.enabled = false;
            _deathsText.enabled = false;
            _scoreText.enabled = false;
        }
    }
}