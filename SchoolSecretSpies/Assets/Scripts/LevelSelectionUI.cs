using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _scene;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(_scene);
    }
}
