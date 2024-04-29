using System;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private KeyType _keyType;
    public static Action<KeyType> OnCollectKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bool hasKey = false;

            foreach (KeyType key in _gameManager.Keys)
            {
                if (key == _keyType)
                {
                    hasKey = true;
                }
            }

            if (hasKey)
            {
                Destroy(gameObject);
            }
        }
    }
}