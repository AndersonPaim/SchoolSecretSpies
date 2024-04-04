using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static Action OnFindPlayer;

    [SerializeField] protected GameObject _target;
    [SerializeField] protected GameObject _fovTrigger;
    [SerializeField] protected Transform _fovPivot;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected LayerMask _raycastLayer;
    [SerializeField] protected float _moveSpeed;

    protected bool _canSeePlayer = false;

    public void DisableEnemy(float disableDuration)
    {
        _fovPivot.gameObject.SetActive(false);
        _animator.SetFloat("Speed", 0);

        StartCoroutine(DisableDelay());

        IEnumerator DisableDelay()
        {
            yield return new WaitForSeconds(disableDuration);
            _fovPivot.gameObject.SetActive(true);
            _animator.SetFloat("Speed", 1);
        }
    }

    protected virtual void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 targetDir = _target.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, Mathf.Infinity, _raycastLayer);

        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player.IsHide)
            {
                return;
            }

            _canSeePlayer = true;
            OnFindPlayer?.Invoke();
            Debug.DrawRay(transform.position, targetDir, Color.blue, 1);
        }

        Ammo ammo = other.gameObject.GetComponent<Ammo>();

        if (ammo != null)
        {
            DisableEnemy(ammo.DisableDuration);
        }
    }
}
