using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _goodTime;
    [SerializeField] private float _averageTime;
    [SerializeField] private float _badTime;
    [SerializeField] private float _collectablesTime;

    public Action<int> OnUpdateUI;

    private int _points;
    public int Points => _points;

    public string GetFinalScore(float time, float lifes)
    {
        time -= _collectablesTime * _points;
        float score = 0;

        if (time <= _goodTime)
        {
            score += 3;
        }
        else if (time > _averageTime)
        {
            score += 2;
        }
        else if (time > _badTime)
        {
            score += 1;
        }

        score += lifes;

        if (score >= 6)
        {
            return "A+";
        }
        else if (score >= 4)
        {
            return "A";
        }
        else if (score >= 2)
        {
            return "B";
        }

        return "C";
    }

    private void Start()
    {
        SetupEvents();
    }

    private void OnDestroy()
    {
        DestroyEvents();
    }

    private void SetupEvents()
    {
        ItemCollectable.OnCollect += CollectItem;
    }

    private void DestroyEvents()
    {
        ItemCollectable.OnCollect -= CollectItem;
    }

    private void CollectItem(int points)
    {
        _points += points;
        OnUpdateUI?.Invoke(_points);
    }
}