using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _volumeSlider.onValueChanged.AddListener(HandleVolumeSliderChange);
        _quitButton.onClick.AddListener(HandleQuitButtonClick);
    }

    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void HandleVolumeSliderChange(float volume)
    {
        _audioMixer.SetFloat("globalVolume", Mathf.Log10(volume) * 20);
    }

    private void HandleQuitButtonClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
