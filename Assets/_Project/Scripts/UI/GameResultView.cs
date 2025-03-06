using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameEndPanel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _gameStateText;
        
        private const string GameOverText = "Game Over";
        private const string GameWonText = "Game Won";

        public void ShowGameOverPanel(Action onRestart)
        {
            _gameEndPanel.SetActive(true);
            _gameStateText.text = GameOverText;
            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(() => onRestart?.Invoke());
        }
        
        public void ShowGameWonPanel(Action onRestart)
        {
            _gameEndPanel.SetActive(true);
            _gameStateText.text = GameWonText;
            _restartButton.onClick.RemoveAllListeners();
            _restartButton.onClick.AddListener(() => onRestart?.Invoke());
        }
        
        public void HideGameEndPanel()
        {
            _gameEndPanel.SetActive(false);
        }
    }
}