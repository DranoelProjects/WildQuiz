using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int hearts = 0;
    private static int coins = 0;
    private static int numberOfLevelsBeforeAd = 6;
    private static int currentNumberOfLevels; 

    //Data recovery from player prefs about hearts, coins and the number of levels done without AD
    static GameData()
    {
        hearts = PlayerPrefs.GetInt("HeartsNumber");
        coins = PlayerPrefs.GetInt("CoinsNumber");
        currentNumberOfLevels = PlayerPrefs.GetInt("CurrentLevelsBeforeAd");
    }

    //Get and set for hearts
    public static int Hearts
    {
        get { return hearts;  }
        set {
            PlayerPrefs.SetInt("HeartsNumber", (hearts = value));
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateHearts();
        }
    }

    //Get and set for coins
    public static int Coins
    {
        get { return coins; }
        set {
            PlayerPrefs.SetInt("CoinsNumber", (coins = value));
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
        }
    }

    //Get and set for the number of levels done without AD
    public static int CurrentNumberOfLevelsBeforeAd
    {
        get { return currentNumberOfLevels; }
        set { 
            PlayerPrefs.SetInt("CurrentLevelsBeforeAd", (currentNumberOfLevels = value));
            if (currentNumberOfLevels == numberOfLevelsBeforeAd)
            {
                currentNumberOfLevels = 0;
                GameObject.Find("GameManager").GetComponent<WatchAd>().ShowVideoAfterXLevels();
            }
        }
    }
}
