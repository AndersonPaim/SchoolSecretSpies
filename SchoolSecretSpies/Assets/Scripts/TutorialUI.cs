using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject _hud;
    [SerializeField] private Button _playButton;
    [SerializeField] private bool _enableOnStart;

    private void Start()
    {
        if(_enableOnStart)
        {
            Time.timeScale = 0;
            gameObject.transform.DOScale(1, 0.2f).SetUpdate(true);
        }

        _playButton.onClick.AddListener(HandlePlayButtonClicked);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
    }

    private void HandlePlayButtonClicked()
    {
        Time.timeScale = 1;
        _hud.SetActive(true);
        gameObject.SetActive(false);
    }
}
