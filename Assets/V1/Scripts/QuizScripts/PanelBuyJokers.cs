using UnityEngine;

public class PanelBuyJokers : MonoBehaviour
{
    QuizManager quizManager;
    UIScript uiScript;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWin;

    private void Awake()
    {
        quizManager = GameObject.Find("QuizManager").GetComponent<QuizManager>();
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        audioSource = quizManager.GetComponent<AudioSource>();
    }
    
    // Buy delete one wrong answer wildcard
    public void BuyDeleteOneWrongJoker()
    {
        if (GameDataV2.Coins >= 20)
        {
            GameDataV2.Coins -= 20;
            uiScript.UpdateCoins();
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
        if (GameDataV2.Coins >= 35)
        {
            GameDataV2.Coins -= 35;
            uiScript.UpdateCoins();
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
        if (GameDataV2.Coins >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            GameDataV2.Coins -= 150;
            GameDataV2.NumberWonLevels++;
            uiScript.UpdateCoins();
            quizManager.ShowWinningCoins(false);
            quizManager.RevealAnswer(true);
            quizManager.OnClickShowJokersPanel();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }

    // Buy clue
    public void BuyClue()
    {
        if (GameDataV2.Coins >= 20)
        {
            GameDataV2.Coins -= 20;
            uiScript.UpdateCoins();
            quizManager.ShowCluePanel();
            quizManager.OnClickShowJokersPanel();
        } else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }


}
