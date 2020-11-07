using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyJokers : MonoBehaviour
{
    QuizManager quizManager;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWin;

    private void Awake()
    {
        quizManager = GameObject.Find("QuizManager").GetComponent<QuizManager>();
        audioSource = quizManager.GetComponent<AudioSource>();
    }


    public void BuyDeleteOneWrongJoker()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 20)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 20);
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
            quizManager.DeleteWrongAnswers(1);
            quizManager.DisableBuyButton();
        } else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }

    }

    public void BuyDeleteTwoWrongJoker()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 35)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 35);
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
            quizManager.DeleteWrongAnswers(2);
            quizManager.DisableBuyButton();
        } else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }
    }

    public void BuyGoToNextLevel()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 150);
            PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
            quizManager.ShowWinningCoins(false);
            quizManager.RevealAnswer();
            quizManager.OnClickShowJokersPanel();
        } else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }
    }

    public void BuyClue()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 20)
        {
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 20);
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
            quizManager.ShowCluePanel();
            quizManager.OnClickShowJokersPanel();
        } else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }
    }


}
