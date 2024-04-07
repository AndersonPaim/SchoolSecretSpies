using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private GameObject _ammoGameObject;
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private float _shotForce;
    [SerializeField] private float _shotPathDistance;

    private bool _isAiming = false;
    private Vector3 _mousePos;

    private void Update()
    {
        Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAiming)
        {
            Shot();
        }
    }

    private void Shot()
    {
        GameObject ammo = Instantiate(_ammoGameObject, transform.position, Quaternion.identity);
        Rigidbody2D ammoRB = ammo.GetComponent<Rigidbody2D>();
        ammoRB.AddForce(GetShotDirection() * _shotForce);
    }

    private Vector3 GetShotDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0;
        return direction.normalized;
    }
}
