using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int hearts = 0;
    private static int coins = 0;

    static GameData()
    {
        hearts = PlayerPrefs.GetInt("HeartsNumber", 0);
        coins = PlayerPrefs.GetInt("CoinsNumber", 0);
    }

    public static int Hearts
    {
        get { return hearts;  }
        set { PlayerPrefs.SetInt("HeartsNumber", (hearts = value)); }
    }

    public static int Coins
    {
        get { return coins; }
        set { PlayerPrefs.SetInt("CoinsNumber", (coins = value)); }
    }
}
