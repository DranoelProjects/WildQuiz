using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameDataV2
{
    private static int nextLevel; // Next level => Never played
    private static int currentLevel; // Loaded level => Can be already played
    private static Level currentLevelData;

    // user utils
    private static bool mute;
    private static int coins;
    private static int hearts;

    // technicals utils
    private static string serviceCallingWatchAd;

    // Stats
    private static int numberLostLevels;
    private static int numberWonLevels;



    static GameDataV2()
    {
        //PlayerPrefs.DeleteAll();
        //Testing if this is the player first connexion
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("RewardClaimDatetime")))
        {
            PlayerPrefs.SetString("RewardClaimDatetime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("NextLevel", 1);
            nextLevel = 1;
            PlayerPrefs.SetInt("HeartsNumber", 20);
            Hearts = 20;
            PlayerPrefs.SetInt("CoinsNumber", 100);
            coins = 100;
            PlayerPrefs.SetInt("NumberLostLevels", 0);
            NumberLostLevels = 0;
            PlayerPrefs.SetInt("NumberWonLevels", 0);
            NumberWonLevels = 0;
            PlayerPrefs.SetInt("mute", 0);
            Mute = false;
        } else
        {
            nextLevel = PlayerPrefs.GetInt("NextLevel");
            coins = PlayerPrefs.GetInt("CoinsNumber");
            Hearts = PlayerPrefs.GetInt("HeartsNumber");
            NumberLostLevels = PlayerPrefs.GetInt("NumberLostLevels");
            NumberWonLevels = PlayerPrefs.GetInt("NumberWonLevels");
            Mute = (PlayerPrefs.GetInt("mute") == 1) ? true : false;
        }
    }

    public static int NextLevel { get => nextLevel; set => nextLevel = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static Level CurrentLevelData { get => currentLevelData; set => currentLevelData = value; }
    public static bool Mute { get => mute; set => mute = value; }
    public static int Coins { get => coins; set => coins = value; }
    public static int Hearts { get => hearts; set => hearts = value; }

    public static string ServiceCallingWatchAd { get => serviceCallingWatchAd; set => serviceCallingWatchAd = value; }
    public static int NumberLostLevels { get => numberLostLevels; set => numberLostLevels = value; }
    public static int NumberWonLevels { get => numberWonLevels; set => numberWonLevels = value; }

}
