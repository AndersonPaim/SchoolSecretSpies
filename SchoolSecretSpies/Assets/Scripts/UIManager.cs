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
    [SerializeField] private KeyItemUI _keyItemUI;
    [SerializeField] private Transform _keyItemPosition;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _slingshotCooldownImage;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lifesText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private CanvasGroup _fadingCanvas;

    public void FadeUI(float endValue, float duration)
    {
        _fadingCanvas.DOFade(endValue, duration).SetUpdate(true);
    }

    private void Start()
    {
        _scoreManager.OnUpdateUI += UpdateScoreUI;
        _gameManager.OnUpdateLifes += UpdateLifesUI;
        _gameManager.OnUpdateTimer += UpdateTimerUI;
        _gameManager.Slingshot.OnShoot += HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot += EnableSlingshotUI;
        KeyTrigger.OnCollectKey += CollectKeyUI;

        if (_gameManager.StartWithSlingshot)
        {
            _slingShotUI.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _scoreManager.OnUpdateUI -= UpdateScoreUI;
        _gameManager.OnUpdateLifes -= UpdateLifesUI;
        _gameManager.OnUpdateTimer -= UpdateTimerUI;
        _gameManager.Slingshot.OnShoot -= HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot -= EnableSlingshotUI;
        KeyTrigger.OnCollectKey -= CollectKeyUI;
    }

    private void UpdateScoreUI(int score)
    {
        _scoreText.text = "Collectables:" + score;
    }

    private void EnableSlingshotUI(bool showTutorial)
    {
        _slingShotUI.SetActive(true);

        if (!showTutorial)
        {
            return;
        }

        _slingShotTutorialUI.transform.DOScale(1, 0.2f).SetUpdate(true);
    }

    private void CollectKeyUI(KeyType keyType, Color color)
    {
        KeyItemUI itemUI = Instantiate(_keyItemUI, _keyItemPosition);
        itemUI.SetColor(color);
    }

    private void UpdateLifesUI(int lifes)
    {
        _lifesText.text = "LIFES:" + lifes;
    }

    private void UpdateTimerUI(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.FloorToInt(totalSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((totalSeconds * 1000f) % 1000f);

        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        _timerText.text = timeText;
    }

    private void HandleSlingshotShoot(float ammo, float cooldown)
    {
        _ammoText.text = ammo.ToString();
        _slingshotCooldownImage.fillAmount = 1;
        DOTween.To(() => _slingshotCooldownImage.fillAmount, x => _slingshotCooldownImage.fillAmount = x, 0, cooldown);
    }
}
