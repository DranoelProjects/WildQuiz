using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameDataV2
{
    private static int nextLevel; // Next level => Never played
    private static int currentLevel; // Loaded level => Can be already played
    private static Level currentLevelData;

    static GameDataV2()
    {
        //PlayerPrefs.DeleteAll();
        //Testing if this is the player first connexion
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("RewardClaimDatetime")))
        {
            PlayerPrefs.SetString("RewardClaimDatetime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("NextLevel", 1);
            PlayerPrefs.SetInt("HeartsNumber", 10);
            PlayerPrefs.SetInt("CoinsNumber", 100);
            PlayerPrefs.SetInt("NumberLostLevels", 0);
            PlayerPrefs.SetInt("NumberWonLevels", 0);
            PlayerPrefs.SetInt("mute", 0);
        }
        nextLevel = PlayerPrefs.GetInt("NextLevel");
    }

    public static int NextLevel { get => nextLevel; set => nextLevel = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static Level CurrentLevelData { get => currentLevelData; set => currentLevelData = value; }
}
