using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            _enemy.DealDamage(player);
        }
    }
}
