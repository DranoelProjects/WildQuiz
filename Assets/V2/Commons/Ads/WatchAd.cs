using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class WatchAd : MonoBehaviour, IUnityAdsListener
{
    string gameId = "3885299";
    bool testMode = false;
    string rewardedVideoId = "rewardedVideo"; // One heart rewarded video
    string coinsRewardedVideoId = "coinsMultiplierRewardedVideo"; // Coins multiplier at the end of a level
    string videoAfterXLevelsId = "videoAfterXLevels"; // Ad after x levels
    NextSceneScript panelNextSceneScript;
    UIScript uiScript;
    private void Awake()
    {
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
    }

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
    }

    public void ShowRewardedVideo(string type)
    {
        StartCoroutine(ShowRewardedAdWhenReady(type));
    }

    /*public void ShowVideoAfterXLevels()
    {
        StartCoroutine(ShowAdWhenReady());
    }*/

    IEnumerator ShowRewardedAdWhenReady(string type)
    {
        while (!Advertisement.IsReady(rewardedVideoId) && !Advertisement.IsReady(coinsRewardedVideoId))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Show(type);
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady(videoAfterXLevelsId))
        {
            yield return new WaitForSeconds(0.25f);
        }
        Advertisement.Show(videoAfterXLevelsId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if (placementId == rewardedVideoId)
            {
                //Heart
                GameDataV2.Hearts += 1;
                uiScript.UpdateHearts();
            }
            else if (placementId == coinsRewardedVideoId)
            {
                //Coins 
                if(GameDataV2.ServiceCallingWatchAd == "QuizManager")
                {
                    GameObject.Find("QuizManager").GetComponent<QuizManager>().OnAdFinished();
                } else
                {
                    GameObject.Find("PanelNextScene").GetComponent<NextSceneScript>().OnAdFinished();
                }
                uiScript.UpdateCoins();
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}