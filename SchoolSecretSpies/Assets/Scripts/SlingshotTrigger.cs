using System;
using UnityEngine;

public class SlingshotTrigger : MonoBehaviour
{
    [SerializeField] private bool _showTutorial;
    public static Action<bool> OnCollectSlingshot;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_showTutorial)
            {
                Time.timeScale = 0;
            }

            OnCollectSlingshot?.Invoke(_showTutorial);
            Destroy(gameObject);
        }
    }
}
