using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lifesText;

    private void Start()
    {
        _scoreManager.OnUpdateUI += UpdateScoreUI;
        _gameManager.OnUpdateLifes += UpdateLifesUI;
    }

    private void OnDestroy()
    {
        _scoreManager.OnUpdateUI -= UpdateScoreUI;
        _gameManager.OnUpdateLifes -= UpdateLifesUI;
    }

    private void UpdateScoreUI(int score)
    {
        _scoreText.text = "SCORE:" + score;
    }

    private void UpdateLifesUI(int lifes)
    {
        _lifesText.text = "LIFES:" + lifes;
    }
}
