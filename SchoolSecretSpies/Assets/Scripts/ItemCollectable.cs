using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollectable : MonoBehaviour
{
    [SerializeField] private GameObject _timeAnimatedText;
    [SerializeField] private int _time;

    public static Action<int> OnCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject timeAnimatedText = Instantiate(_timeAnimatedText);
            timeAnimatedText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("-" + _time + " SEC");
            timeAnimatedText.transform.position = transform.position;
            OnCollect?.Invoke(_time);
            gameObject.SetActive(false);
        }
    }
}
