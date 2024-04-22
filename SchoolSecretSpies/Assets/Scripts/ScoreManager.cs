using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Action<int> OnUpdateUI;

    private int _points;
    public int Points => _points;

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