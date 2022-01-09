using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataV2
{
    private static int currentPage;
    private static int nextLevel; // Next level => Never played
    private static int currentLevel; // Loaded level => Can be already played
    private static Level currentLevelData;

    static GameDataV2()
    {
        currentPage = PlayerPrefs.GetInt("CurrentPage");
        nextLevel = PlayerPrefs.GetInt("NextLevel");
    }

    public static int CurrentPage { get => currentPage; set => currentPage = value; }
    public static int NextLevel { get => nextLevel; set => nextLevel = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static Level CurrentLevelData { get => currentLevelData; set => currentLevelData = value; }
}
