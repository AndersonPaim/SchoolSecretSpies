using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, LevelData> LevelData = new Dictionary<string, LevelData>();
}


[System.Serializable]
public class LevelData
{
    public List<DeathData> Deaths = new List<DeathData>();
    public List<float> Time = new List<float>();
    public float Collectables;
    public float Shots;
    public float HidePlaces;
    public float TimesPlayed;
}

[System.Serializable]
public class DeathData
{
    public float PosX;
    public float PosY;
    public EnemyType EnemyType;
}

public enum EnemyType
{
    Guard, Camera
}
