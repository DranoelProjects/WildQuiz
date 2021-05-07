using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUserInfo : MonoBehaviour
{
    [SerializeField] Text heartsNumber, coinsNumber, outputCurrentLvl;
    [SerializeField] Image heart;
    [SerializeField] Sprite redHeart, greyHeart;
    WatchAd watchAd;

    private void Awake() {
        watchAd = GameObject.Find("GameManager").GetComponent<WatchAd>();
    }
    private void Start()
    {
        UpdateCoins();
        UpdateHearts();
        UpdateCurrentLvl();
    }

    public void UpdateHearts()
    {
        if (!heartsNumber)
        {
            heartsNumber = GameObject.Find("HeartsNumber").GetComponent<Text>();
        }
        heartsNumber.text = PlayerPrefs.GetInt("HeartsNumber").ToString();

        if (PlayerPrefs.GetInt("HeartsNumber") != 0)
        {
            heart.sprite = redHeart;
        }
        else
        {
            heart.sprite = greyHeart;
        }
    }

    public void UpdateCoins()
    {
        coinsNumber.text = PlayerPrefs.GetInt("CoinsNumber").ToString();
    }

    public void UpdateCurrentLvl()
    {
        outputCurrentLvl.text = PlayerPrefs.GetInt("NextLevel").ToString();
    }

    //Reward : Heart
    public void OnClickShowRewardedVideo()
    {
        watchAd.ShowRewardedVideo("rewardedVideo");
    }
}
