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

    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private int lifes;

    private void Start()
    {
        Enemy.OnFindPlayer += GameOver;
    }

    private void OnDestroy()
    {
        Enemy.OnFindPlayer -= GameOver;
    }

    private void GameOver()
    {
        lifes--;
        OnUpdateLifes?.Invoke(lifes);

        if (lifes == 0)
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