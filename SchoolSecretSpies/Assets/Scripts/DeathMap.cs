using UnityEngine;
using TMPro;

public class DeathMap : MonoBehaviour
{

    [SerializeField] private GameObject _deathPoint;
    [SerializeField] private string _levelKey;
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _avgTimeText;
    [SerializeField] private TextMeshProUGUI _avgShotText;
    [SerializeField] private TextMeshProUGUI _avgCollectablesText;
    [SerializeField] private TextMeshProUGUI _avgHidesText;
    [SerializeField] private TextMeshProUGUI _avgDeathsText;

    private void Start()
    {
        SaveSystem.Load();

        foreach (DeathData data in SaveSystem.localData.LevelData[_levelKey].Deaths)
        {
            GameObject deathPoint = Instantiate(_deathPoint);
            deathPoint.transform.position = new Vector3(data.PosX, data.PosY, 0);
        }

        float bestTime = 0;
        float totalTime = 0;

        foreach (float time in SaveSystem.localData.LevelData[_levelKey].Time)
        {
            totalTime += time;

            Debug.Log("TIME: " + time);

            if (bestTime > time || bestTime == 0)
            {
                Debug.Log("TIME2: " + bestTime + " : " + time);
                bestTime = time;
            }
        }

        float timesPlayed = SaveSystem.localData.LevelData[_levelKey].TimesPlayed;

        float avgTime = totalTime / timesPlayed;
        float avgDeaths = SaveSystem.localData.LevelData[_levelKey].Deaths.Count / timesPlayed;
        float avgShots = SaveSystem.localData.LevelData[_levelKey].Shots / timesPlayed;
        float avgHides = SaveSystem.localData.LevelData[_levelKey].HidePlaces / timesPlayed;
        float avgCollectables = SaveSystem.localData.LevelData[_levelKey].Collectables / timesPlayed;

        _bestTimeText.text = "TIME: " + GetTimeText(bestTime);
        _avgTimeText.text = "TIME: " + GetTimeText(avgTime);
        _avgDeathsText.text = "DEATHS: " + avgDeaths;
        _avgHidesText.text = "HIDE PLACES USED: " + avgHides;
        _avgCollectablesText.text = "COLLECTABLES: " + avgCollectables;
    }

    public string GetTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return timeText;
    }

}
