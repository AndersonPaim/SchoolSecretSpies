using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class PuzzlePassword
{
    public string Password;
    public string Tip;
}

public class ComputerPuzzle : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ComputerButton _computerButtonPrefab;
    [SerializeField] private GameObject _errorUI;
    [SerializeField] private GameObject _successUI;
    [SerializeField] private Transform _letterPos;
    [SerializeField] private Button _deleteLetter;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private TextMeshProUGUI _tipText;
    [SerializeField] private List<PuzzlePassword> _passwordList = new List<PuzzlePassword>();

    private List<Button> _lastButtons = new List<Button>();
    private int _randomWordIndex;

    private void Start()
    {
        _randomWordIndex = UnityEngine.Random.Range(0, _passwordList.Count);
        _tipText.text = "TIP: " + _passwordList[_randomWordIndex].Tip;

        char[] characters = _passwordList[_randomWordIndex].Password.ToArray();
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

        if (password[1] == _passwordList[_randomWordIndex].Password)
        {
            StartCoroutine(FinishPuzzle());
        }
    }

    private IEnumerator FinishPuzzle()
    {
        _errorUI.SetActive(false);
        _successUI.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        transform.DOScale(0, 0.3f).SetUpdate(true);
        _gameManager.FinishLevel();
    }
}
