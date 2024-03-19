using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _playButton.onClick.AddListener(HandlePlayButtonClick);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void HandlePlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void HandleQuitButtonClick()
    {
        Application.Quit();
    }
}
