using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _slingShotUI;
    [SerializeField] private GameObject _slingShotTutorialUI;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _slingshotCooldownImage;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lifesText;
    [SerializeField] private CanvasGroup _fadingCanvas;

    public void FadeUI(float endValue, float duration)
    {
        _fadingCanvas.DOFade(endValue, duration).SetUpdate(true);
    }

    private void Start()
    {
        _scoreManager.OnUpdateUI += UpdateScoreUI;
        _gameManager.OnUpdateLifes += UpdateLifesUI;
        _gameManager.Slingshot.OnShoot += HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot += EnableSlingshotUI;

        if (_gameManager.StartWithSlingshot)
        {
            _slingShotUI.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _scoreManager.OnUpdateUI -= UpdateScoreUI;
        _gameManager.OnUpdateLifes -= UpdateLifesUI;
        _gameManager.Slingshot.OnShoot -= HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot -= EnableSlingshotUI;
    }

    private void UpdateScoreUI(int score)
    {
        _scoreText.text = "SCORE:" + score;
    }

    private void EnableSlingshotUI(bool showTutorial)
    {
        if (!showTutorial)
        {
            return;
        }

        _slingShotTutorialUI.transform.DOScale(1, 0.2f).SetUpdate(true);
        _slingShotUI.SetActive(true);
    }

    private void UpdateLifesUI(int lifes)
    {
        _lifesText.text = "LIFES:" + lifes;
    }

    private void HandleSlingshotShoot(float ammo, float cooldown)
    {
        _ammoText.text = ammo.ToString();
        _slingshotCooldownImage.fillAmount = 1;
        DOTween.To(() => _slingshotCooldownImage.fillAmount, x => _slingshotCooldownImage.fillAmount = x, 0, cooldown);
    }
}
