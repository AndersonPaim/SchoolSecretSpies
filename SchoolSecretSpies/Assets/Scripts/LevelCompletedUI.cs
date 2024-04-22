using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private string _nextLevel;

    private void Start()
    {
        _playAgainButton.onClick.AddListener(HandlePlayButtonClick);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
    }

    private void OnDestroy()
    {
        _playAgainButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void HandlePlayButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_nextLevel);
    }

    private void HandleQuitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
