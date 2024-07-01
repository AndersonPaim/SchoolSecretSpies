using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _levelSelectionUI;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        SaveSystem.Load();
        _playButton.onClick.AddListener(HandlePlayButtonClick);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
        _optionsButton.onClick.AddListener(HandleOptionsButtonClicked);

        float volume = SaveSystem.localData.GlobalVolume;
        Debug.Log("VOLUME: " + volume);
        _audioMixer.SetFloat("globalVolume", Mathf.Log10(volume) * 20);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
    }

    private void HandleOptionsButtonClicked()
    {
        _optionsUI.SetActive(true);
    }

    private void HandlePlayButtonClick()
    {
        _levelSelectionUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void HandleQuitButtonClick()
    {
        Application.Quit();
    }
}
