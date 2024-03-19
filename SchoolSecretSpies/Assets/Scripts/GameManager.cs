using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverUI _gameOverUI;

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
        Time.timeScale = 0;
        _gameOverUI.gameObject.SetActive(true);
    }
}