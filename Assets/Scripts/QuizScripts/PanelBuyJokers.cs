using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyJokers : MonoBehaviour
{
    QuizManager quizManager;
    PanelUserInfo panelUserInfo;
    UIScript uiScript;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWin;

    private void Awake()
    {
        quizManager = GameObject.Find("QuizManager").GetComponent<QuizManager>();
        panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        audioSource = quizManager.GetComponent<AudioSource>();
    }
    
    // Buy delete one wrong answer wildcard
    public void BuyDeleteOneWrongJoker()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 20)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 20);
            panelUserInfo.UpdateCoins();
            quizManager.DeleteWrongAnswers(1);
            quizManager.DisableBuyButton();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }

    }

    // Buy delete two wrong answers wildcard
    public void BuyDeleteTwoWrongJoker()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 35)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 35);
            panelUserInfo.UpdateCoins();
            quizManager.DeleteWrongAnswers(2);
            quizManager.DisableBuyButton();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }

    // Buy the level
    public void BuyGoToNextLevel()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 150);
            PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
            panelUserInfo.UpdateCoins();
            quizManager.ShowWinningCoins(false);
            quizManager.RevealAnswer();
            quizManager.OnClickShowJokersPanel();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }

    // Buy clue
    public void BuyClue()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 20)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 20);
            panelUserInfo.UpdateCoins();
            quizManager.ShowCluePanel();
            quizManager.OnClickShowJokersPanel();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }


}
