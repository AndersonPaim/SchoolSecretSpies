using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletedUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _levelCompletedUI.SetActive(true);
        Time.timeScale = 0;
    }
}
