using System;
using System.Collections;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public static Action<float, float> OnShot;

    [SerializeField] private GameObject _ammoGameObject;
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _shootCooldown;
    [SerializeField] private float _initialAmmo;

    private bool _isAiming = false;
    private bool _canShoot = true;
    private bool _isHide = false;
    private float _ammo;

    public void Hide(bool isHide)
    {
        _isHide = isHide;
    }

    private void Start()
    {
        _ammo = _initialAmmo;
        OnShot?.Invoke(_ammo, 0);
    }

    private void Update()
    {
        Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_isHide)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            _crosshair.SetActive(true);
            _crosshair.transform.position = new Vector3(_mousePos.x, _mousePos.y, 0);
            _isAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _crosshair.SetActive(false);
            _isAiming = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_canShoot || _ammo <= 0)
        {
            return;
        }

        GameObject ammo = Instantiate(_ammoGameObject, transform.position, Quaternion.identity);
        Rigidbody2D ammoRB = ammo.GetComponent<Rigidbody2D>();
        ammoRB.AddForce(GetShotDirection() * _shootForce);
        _ammo--;

        OnShot?.Invoke(_ammo, _shootCooldown);

        StartCoroutine(ShotCooldown());

        IEnumerator ShotCooldown()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_shootCooldown);
            _canShoot = true;
        }
    }

    private Vector3 GetShotDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0;
        return direction.normalized;
    }
}
