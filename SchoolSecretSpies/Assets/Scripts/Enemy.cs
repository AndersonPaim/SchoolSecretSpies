using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public static Action OnFindPlayer;

    [SerializeField] protected Collider2D _damageTrigger;
    [SerializeField] protected GameObject _target;
    [SerializeField] protected GameObject _fovTrigger;
    [SerializeField] protected Transform _fovPivot;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected LayerMask _raycastLayer;
    [SerializeField] protected float _moveSpeed;

    protected bool _canSeePlayer = false;

    public void TakeDamage(float damage)
    {
        DisableEnemy(damage);
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
    }

    private void DisableEnemy(float disableDuration)
    {
        _fovPivot.gameObject.SetActive(false);
        _damageTrigger.enabled = false;
        _animator.SetFloat("Speed", 0);

        StartCoroutine(DisableDelay());

        IEnumerator DisableDelay()
        {
            yield return new WaitForSeconds(disableDuration);
            _fovPivot.gameObject.SetActive(true);
            _damageTrigger.enabled = true;
            _animator.SetFloat("Speed", 1);
        }
    }
}
