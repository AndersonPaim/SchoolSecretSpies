using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static Action OnFindPlayer;

    [SerializeField] protected GameObject _target;
    [SerializeField] protected GameObject _fovTrigger;
    [SerializeField] protected Transform _fovPivot;
    [SerializeField] protected LayerMask _raycastLayer;
    [SerializeField] protected float _moveSpeed;

    protected bool _canSeePlayer = false;

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
            _canSeePlayer = true;
            OnFindPlayer?.Invoke();
            Debug.DrawRay(transform.position, targetDir, Color.blue, 1);
        }
    }
}
