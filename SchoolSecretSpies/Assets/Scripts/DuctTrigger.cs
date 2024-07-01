using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuctTrigger : MonoBehaviour
{
    [SerializeField] private Transform _teleportPos;
    [SerializeField] private GameObject _buttonIcon;
    [SerializeField] private bool _canEnter = true;
    public Action<PlayerController, Transform> OnTrigger;

    private bool _isTrigger;
    private PlayerController _player;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canEnter)
        {
            return;
        }

        _player = other.GetComponent<PlayerController>();

        if (_player != null)
        {
            _isTrigger = true;
            _buttonIcon.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        _player = other.GetComponent<PlayerController>();

        if (_player != null)
        {
            _isTrigger = false;
            _buttonIcon.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isTrigger)
        {
            OnTrigger?.Invoke(_player, _teleportPos);
        }
    }
}
