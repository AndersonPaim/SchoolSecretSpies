using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyType
{
    Yellow, Red, Blue, Green, Purple
}

public class KeyTrigger : MonoBehaviour
{
    [SerializeField] private KeyType _keyType;
    public static Action<KeyType> OnCollectKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnCollectKey?.Invoke(_keyType);
            Destroy(gameObject);
        }
    }
}
