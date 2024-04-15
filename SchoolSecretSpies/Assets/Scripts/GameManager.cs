using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public Action<int> OnUpdateLifes;

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private GameObject _levelCompletedUI;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slingshot _slingshot;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private int _lifes;
    [SerializeField] private bool _startWithSlingshot;

    public Slingshot Slingshot => _slingshot;

    public bool StartWithSlingshot => _startWithSlingshot;

    public void FinishLevel()
    {
        Debug.Log("FINISH LEVEL");
        //TODO GET SCORE TO SHOW IN THE UI
        _levelCompletedUI.SetActive(true);
    }

    private void Start()
    {
        Enemy.OnFindPlayer += GameOver;
        SlingshotTrigger.OnCollectSlingshot += CollectSlingshot;

        if (_startWithSlingshot)
        {
            CollectSlingshot(false);
        }
    }

    private void OnDestroy()
    {
        Enemy.OnFindPlayer -= GameOver;
        SlingshotTrigger.OnCollectSlingshot -= CollectSlingshot;
    }

    private void CollectSlingshot(bool showTutorial)
    {
        _slingshot.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        _lifes--;
        OnUpdateLifes?.Invoke(_lifes);

        if (_lifes == 0)
        {
            Time.timeScale = 0;
            _gameOverUI.gameObject.SetActive(true);
        }
        else
        {
            _playerController.gameObject.transform.DOMove(_respawnPoint.position, 0);
        }
    }
}