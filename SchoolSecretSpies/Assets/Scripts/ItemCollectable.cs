using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    [SerializeField] private int _points;

    public static Action<int> OnCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCollect?.Invoke(_points);
            gameObject.SetActive(false);
        }
    }
}
