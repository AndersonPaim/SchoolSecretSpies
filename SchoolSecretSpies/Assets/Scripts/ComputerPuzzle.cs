using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerPuzzle : MonoBehaviour
{
    [SerializeField] private ComputerButton _computerButtonPrefab;
    [SerializeField] private GameObject _errorUI;
    [SerializeField] private GameObject _successUI;
    [SerializeField] private Transform _letterPos;
    [SerializeField] private Button _deleteLetter;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private string _password;

    private List<Button> _lastButtons = new List<Button>();

    private void Start()
    {
        char[] characters = _password.ToArray();
        Array.Sort(characters);
        string letterOrder = new string(characters);

        _deleteLetter.onClick.AddListener(HandleDeleteButtonClicked);

        for (int i = 0; i < letterOrder.Length; i++)
        {
            ComputerButton computerButton = Instantiate(_computerButtonPrefab, _letterPos);
            computerButton.Initialize(letterOrder[i]);
            computerButton.OnClick += HandleButtonClicked;
        }
    }

    private void OnDestroy()
    {
        _deleteLetter.onClick.RemoveAllListeners();
    }

    private void HandleDeleteButtonClicked()
    {
        string[] password = _passwordText.text.Split(": ");

        if (password[1].Length > 0)
        {
            _lastButtons[_lastButtons.Count - 1].interactable = true;
            _lastButtons.RemoveAt(_lastButtons.Count - 1);
            password[1] = password[1].Substring(0, _lastButtons.Count);
            _passwordText.text = "PASSWORD: " + password[1];
        }
    }

    private void HandleButtonClicked(string letter, Button button)
    {
        _lastButtons.Add(button);
        _passwordText.text += letter;

        string[] password = _passwordText.text.Split(": ");

        if (password[1] == _password)
        {
            _errorUI.SetActive(false);
            _successUI.SetActive(true);
        }
    }
}
