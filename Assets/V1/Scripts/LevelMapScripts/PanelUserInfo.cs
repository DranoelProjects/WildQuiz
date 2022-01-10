using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUserInfo : MonoBehaviour
{
    [SerializeField] Text heartsNumber, coinsNumber, outputCurrentLvl;
    [SerializeField] Image heart;
    [SerializeField] Sprite redHeart, greyHeart;

    private void Start()
    {
        UpdateCoins();
        UpdateHearts();
        UpdateCurrentLvl();
    }

    // Can be called to update the hearts number
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

    // Can be called to update the coins number
    public void UpdateCoins()
    {
        coinsNumber.text = PlayerPrefs.GetInt("CoinsNumber").ToString();
    }

    // Can be called to update the current lvl number
    public void UpdateCurrentLvl()
    {
        int levels = PlayerPrefs.GetInt("NextLevel");
        outputCurrentLvl.text = levels.ToString();
    }
}
