using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ComputerButton : MonoBehaviour
{
    public Action<string, Button> OnClick;

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _letterText;

    public void Initialize(char letter)
    {
        _letterText.text = letter.ToString();
        _button.onClick.AddListener(HandleButtonClicked);
    }

    public void Reset()
    {
        _button.interactable = true;
    }

    private void HandleButtonClicked()
    {
        OnClick?.Invoke(_letterText.text, _button);
        _button.interactable = false;
    }
}
