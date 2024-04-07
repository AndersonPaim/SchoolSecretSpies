using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float _disableDuration;

    public float DisableDuration => _disableDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_disableDuration);
        }
    }
}
