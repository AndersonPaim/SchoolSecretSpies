using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _computerPuzzle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(OpenComputer());

        IEnumerator OpenComputer()
        {
            Time.timeScale = 0;
            _uiManager.FadeUI(1, 0.3f);
            yield return new WaitForSecondsRealtime(0.3f);
            _computerPuzzle.SetActive(true);
            _uiManager.FadeUI(0, 0.3f);
        }
    }
}
