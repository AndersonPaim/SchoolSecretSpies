using System;
using UnityEngine;

public enum KeyType
{
    Yellow, Red, Blue, Green, Purple
}

public class KeyTrigger : MonoBehaviour
{
    public static Action<KeyType, Color> OnCollectKey;

    [SerializeField] private KeyType _keyType;
    [SerializeField] private Color _color;

    private bool _canTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _canTrigger)
        {
            _canTrigger = false;
            OnCollectKey?.Invoke(_keyType, _color);
            Destroy(gameObject);
        }
    }
}
