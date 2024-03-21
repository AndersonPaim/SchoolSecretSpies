using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    Rigidbody2D _rb;

    private bool _isHide = true;
    public bool IsHide => _isHide;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        HideableObject.OnHide += Hide;
    }

    private void OnDestroy()
    {
        HideableObject.OnHide -= Hide;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _rb.velocity = new Vector2(0, 0);

        Vector2 newVelocity = _rb.velocity;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newVelocity.x = -_moveSpeed;
            _animator.SetInteger("DirectionX", -1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newVelocity.x = _moveSpeed;
            _animator.SetInteger("DirectionX", 1);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newVelocity.y = _moveSpeed;
            _animator.SetInteger("DirectionY", 1);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newVelocity.y = -_moveSpeed;
            _animator.SetInteger("DirectionY", -1);
        }

        //KEY UP

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetInteger("DirectionX", 0);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetInteger("DirectionX", 0);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            _animator.SetInteger("DirectionY", 0);
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _animator.SetInteger("DirectionY", 0);
        }

        _rb.velocity = newVelocity;
    }

    private void Hide(bool isHide)
    {
        _playerSprite.enabled = !isHide;
        _isHide = isHide;
    }
}
