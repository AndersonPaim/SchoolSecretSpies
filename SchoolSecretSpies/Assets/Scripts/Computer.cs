using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject _computerPuzzle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _computerPuzzle.SetActive(true);
        Time.timeScale = 0;
    }
}
