using System;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemUI : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void SetColor(Color color)
    {
        _icon.color = color;
    }
}