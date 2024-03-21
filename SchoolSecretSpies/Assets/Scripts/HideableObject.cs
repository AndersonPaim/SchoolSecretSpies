using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableObject : MonoBehaviour
{
    public static Action<bool> OnHide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIDE");
            OnHide?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnHide?.Invoke(false);
        }
    }
}
