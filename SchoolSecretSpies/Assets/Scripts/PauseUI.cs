using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private Button _continue;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _optionsButton;


    private void Start()
    {
        _continue.onClick.AddListener(HandlePlayButtonClick);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
        _optionsButton.onClick.AddListener(HandleOptionsButtonClick);
    }

    private void OnDestroy()
    {
        _continue.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
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

    private void HandleOptionsButtonClick()
    {
        _optionsUI.SetActive(true);
    }

}
