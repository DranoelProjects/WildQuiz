using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class CloudOnceServices : MonoBehaviour
{
    public static CloudOnceServices instance;

    private void Awake() {
        TestSingleton();
    }

    void TestSingleton(){
        if(instance != null){ Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SubmitLevelsToLeaderboard(int levels){
        Leaderboards.hightestLevels.SubmitScore(levels);
    }

    public void SubmitCoinsToLeaderboard(int coins){
        Leaderboards.coinsLeaderboard.SubmitScore(coins);
    }
}
