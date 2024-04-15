using System;
using UnityEngine;

public class SlingshotTrigger : MonoBehaviour
{
    public static Action OnCollectSlingshot;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            OnCollectSlingshot?.Invoke();
            Destroy(gameObject);
        }
    }
}
