using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HideableObject : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _buttonIcon;
    [SerializeField] private Transform _exitPosition;
    public static Action<bool> OnHide;

    private bool _isTrigger;
    private bool _isHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _buttonIcon.SetActive(true);
            _isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnHide?.Invoke(false);
            _buttonIcon.SetActive(false);
            _isTrigger = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isTrigger)
        {
            if (!_isHide)
            {
                _isHide = true;
                _player.transform.DOMove(gameObject.transform.position, 0.2f).OnComplete(() => OnHide?.Invoke(true));
            }
            else
            {
                _isHide = false;
                _player.transform.DOMove(_exitPosition.position, 0.2f).OnComplete(() => OnHide?.Invoke(false));
            }
        }
    }
}
