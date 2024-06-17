using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _quitButton;


    private void Start()
    {
        _continue.onClick.AddListener(HandlePlayButtonClick);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
    }

    private void OnDestroy()
    {
        _continue.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void HandlePlayButtonClick()
    {
        _uiManager.Unpause();
    }

    private void HandleQuitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}
