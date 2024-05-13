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
    public Action<float> OnUpdateTimer;

    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private LevelCompletedUI _levelCompletedUI;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slingshot _slingshot;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private int _lifes;
    [SerializeField] private bool _startWithSlingshot;

    private float _timer = 0;
    private List<KeyType> _keys = new List<KeyType>();

    public Slingshot Slingshot => _slingshot;
    public List<KeyType> Keys => _keys;
    public bool StartWithSlingshot => _startWithSlingshot;
    public int Lifes => _lifes;
    public float Timer => _timer;

    public void FinishLevel()
    {
        Debug.Log("FINISH LEVEL");
        string score = _scoreManager.GetFinalScore(_timer, _lifes);
        _levelCompletedUI.SetScore(score);
        _levelCompletedUI.gameObject.SetActive(true);
    }

    private void Start()
    {
        Enemy.OnFindPlayer += GameOver;
        SlingshotTrigger.OnCollectSlingshot += CollectSlingshot;
        KeyTrigger.OnCollectKey += CollectKey;

        if (_startWithSlingshot)
        {
            CollectSlingshot(false);
        }
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void OnDestroy()
    {
        Enemy.OnFindPlayer -= GameOver;
        SlingshotTrigger.OnCollectSlingshot -= CollectSlingshot;
        KeyTrigger.OnCollectKey -= CollectKey;
    }

    private void CollectSlingshot(bool showTutorial)
    {
        _slingshot.gameObject.SetActive(true);
    }

    private void CollectKey(KeyType keyType, Color color)
    {
        _keys.Add(keyType);
    }

    private void UpdateTimer()
    {
        _timer += Time.deltaTime;
        OnUpdateTimer?.Invoke(_timer);
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
            _playerController.Hide(true);
            _playerController.transform.DOMove(_respawnPoint.position, 2).OnComplete(() => _playerController.Hide(false));
        }
    }
}