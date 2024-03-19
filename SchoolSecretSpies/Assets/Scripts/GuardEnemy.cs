using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GuardEnemy : Enemy
{
    [SerializeField] private List<Transform> _waypointsList = new List<Transform>();

    private int _currentWaypoint = 0;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Movement()
    {
        base.Movement();

        if (_canSeePlayer)
        {
            return;
        }

        Vector3 diff = transform.position - _waypointsList[_currentWaypoint].position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _fovPivot.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);

        transform.position = Vector2.MoveTowards(transform.position, _waypointsList[_currentWaypoint].position, _moveSpeed * Time.deltaTime);

        if (transform.position == _waypointsList[_currentWaypoint].position)
        {
            _currentWaypoint++;

            if (_currentWaypoint >= _waypointsList.Count)
            {
                _currentWaypoint = 0;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
