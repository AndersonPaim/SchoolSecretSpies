using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private GameObject _ammoGameObject;
    [SerializeField] private float _shotForce;
    [SerializeField] private float _disableDuration;

    private bool _isAiming = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _isAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _isAiming = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAiming)
        {
            Shot();
        }
    }

    private void Shot()
    {
        GameObject ammo = Instantiate(_ammoGameObject, transform.position, Quaternion.identity);
        Rigidbody2D ammoRB = ammo.GetComponent<Rigidbody2D>();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        ammoRB.AddForce(direction * _shotForce);
    }
}
