using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelResult : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private TextMeshProUGUI _collectablesText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _lifesText;

    private void Start()
    {
        float totalSeconds = _gameManager.Timer;
        float lifes = _gameManager.Lifes;
        float collectables = _scoreManager.Points;

        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.FloorToInt(totalSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((totalSeconds * 1000f) % 1000f);

        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        _timerText.text = "TIME: " + timeText;
        _collectablesText.text = "COLLECTABLES: " + collectables.ToString();
        _lifesText.text = "LIFES REMAINING: " + lifes.ToString();
    }
}
