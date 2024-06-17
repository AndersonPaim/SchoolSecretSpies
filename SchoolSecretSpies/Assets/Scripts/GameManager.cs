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
    [SerializeField] private string _levelKey;
    [SerializeField] private bool _startWithSlingshot;

    private float _timer = 0;
    private List<KeyType> _keys = new List<KeyType>();

    public List<KeyType> Keys => _keys;
    public bool StartWithSlingshot => _startWithSlingshot;
    public int Lifes => _lifes;
    public float Timer => _timer;

    public void FinishLevel()
    {
        Debug.Log("FINISH LEVEL");
        SaveSystem.localData.LevelData[_levelKey].Time.Add(_timer);
        SaveSystem.localData.LevelData[_levelKey].TimesPlayed++;
        SaveSystem.Save();
        string score = _scoreManager.GetFinalScore(_timer, _lifes);
        _levelCompletedUI.SetScore(score);
        _levelCompletedUI.gameObject.SetActive(true);
    }

    private void Start()
    {
        Enemy.OnFindPlayer += GameOver;
        SlingshotTrigger.OnCollectSlingshot += CollectSlingshot;
        KeyTrigger.OnCollectKey += CollectKey;
        HideableObject.OnHide += Hide;
        Slingshot.OnShot += Shot;
        ItemCollectable.OnCollect += CollectItem;

        SaveSystem.Load();

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
        HideableObject.OnHide -= Hide;
        Slingshot.OnShot -= Shot;
        ItemCollectable.OnCollect -= CollectItem;
    }

    private void CollectSlingshot(bool showTutorial)
    {
        _slingshot.gameObject.SetActive(true);
    }

    private void CollectKey(KeyType keyType, Color color)
    {
        _keys.Add(keyType);
    }

    private void Hide(bool isHide)
    {
        if (!isHide)
        {
            return;
        }

        SaveSystem.localData.LevelData[_levelKey].HidePlaces++;
        SaveSystem.Save();
    }

    private void Shot(float x, float y)
    {
        SaveSystem.localData.LevelData[_levelKey].Shots++;
        SaveSystem.Save();
    }

    private void CollectItem(int time)
    {
        SaveSystem.localData.LevelData[_levelKey].Collectables++;
        SaveSystem.Save();
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

        DeathData deathData = new DeathData();
        deathData.PosX = _playerController.transform.position.x;
        deathData.PosY = _playerController.transform.position.y;
        deathData.EnemyType = EnemyType.Camera;

        Debug.Log("GAME OVER: " + deathData.PosX + " : " + deathData.PosY);
        SaveSystem.localData.LevelData[_levelKey].Deaths.Add(deathData);
        SaveSystem.Save();

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