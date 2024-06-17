using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _slingShotUI;
    [SerializeField] private GameObject _slingShotTutorialUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private KeyItemUI _keyItemUI;
    [SerializeField] private Transform _keyItemPosition;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Image _slingshotCooldownImage;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lifesText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _bonusTimeText;
    [SerializeField] private CanvasGroup _fadingCanvas;

    private bool _isPaused = false;
    private bool _canChangeTimescale;

    private float _totalBonusTime = 0;

    public void Unpause()
    {
        _pauseUI.gameObject.SetActive(false);

        if (_canChangeTimescale)
        {
            Time.timeScale = 1;
        }

        _isPaused = false;
    }

    public void FadeUI(float endValue, float duration)
    {
        _fadingCanvas.DOFade(endValue, duration).SetUpdate(true);
    }

    private void Start()
    {
        _scoreManager.OnUpdateUI += UpdateScoreUI;
        _gameManager.OnUpdateLifes += UpdateLifesUI;
        _gameManager.OnUpdateTimer += UpdateTimerUI;
        Slingshot.OnShot += HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot += EnableSlingshotUI;
        KeyTrigger.OnCollectKey += CollectKeyUI;
        ItemCollectable.OnCollect += Collectable;

        if (_gameManager.StartWithSlingshot)
        {
            _slingShotUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            Unpause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            Pause();
        }
    }

    private void OnDestroy()
    {
        _scoreManager.OnUpdateUI -= UpdateScoreUI;
        _gameManager.OnUpdateLifes -= UpdateLifesUI;
        _gameManager.OnUpdateTimer -= UpdateTimerUI;
        Slingshot.OnShot -= HandleSlingshotShoot;
        SlingshotTrigger.OnCollectSlingshot -= EnableSlingshotUI;
        KeyTrigger.OnCollectKey -= CollectKeyUI;
        ItemCollectable.OnCollect -= Collectable;
    }

    private void Pause()
    {
        if (Time.timeScale == 0)
        {
            _canChangeTimescale = false;
        }
        else
        {
            _canChangeTimescale = true;
        }

        _pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        _isPaused = true;
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

    private void Collectable(int time)
    {
        if (_totalBonusTime == 0)
        {
            _bonusTimeText.gameObject.SetActive(true);
        }

        _totalBonusTime += time;
        _bonusTimeText.text = "-" + GetTimeString(_totalBonusTime);
    }

    private void UpdateLifesUI(int lifes)
    {
        _lifesText.text = lifes.ToString();
    }

    private void UpdateTimerUI(float totalSeconds)
    {
        _timerText.text = GetTimeString(totalSeconds);
    }

    private string GetTimeString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeText;
    }

    private void HandleSlingshotShoot(float ammo, float cooldown)
    {
        _ammoText.text = ammo.ToString();
        _slingshotCooldownImage.fillAmount = 1;
        DOTween.To(() => _slingshotCooldownImage.fillAmount, x => _slingshotCooldownImage.fillAmount = x, 0, cooldown);
    }
}
