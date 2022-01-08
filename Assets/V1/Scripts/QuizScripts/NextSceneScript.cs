using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] GameObject winningCoins, watchAd;
    [SerializeField] Button nextLvlButton;
    [SerializeField] Text textOuputCoinsValue, textMultiplier;
    GameManagerScript gameManagerScript;
    WatchAd watchAdScript;
    UIScript uiScript;
    int randomMultiplier = 0;
    int numberOfCoinsWon = 10;

    private void Awake() {
        GameObject gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        watchAdScript = gameManager.GetComponent<WatchAd>();
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
    }
    public void Start()
    {
        if (PlayerPrefs.GetInt("NextLevel") == 86 && gameManagerScript.LevelData.Level == 85)
        {
            nextLvlButton.interactable = false;
        }
    }

    // Winning coins are just the number of coins that we display in the following scene panel
    public void ActiveWinningCoins(bool active, int value= 10)
    {
        textOuputCoinsValue.text = "+ " + value.ToString();
        winningCoins.SetActive(active);
        numberOfCoinsWon = value;
        randomMultiplier = Random.Range(2, 5);
        textMultiplier.text = randomMultiplier.ToString();
    }

    // On click start the next level
    public void StartNextSceneLevel()
    {
        
        LevelData levelData = gameManagerScript.LevelData;
        LevelData nextLevelData = levelData.NextLevelData;
        if (PlayerPrefs.GetInt("HeartsNumber") > 0)
        {
            gameManagerScript.LevelData = nextLevelData;
            switch (nextLevelData.Type)
            {
                case "Quiz":
                    SceneManager.LoadScene("QuizScene");
                    break;
                case "InputQuiz":
                    SceneManager.LoadScene("QuizScene");
                    break;
                case "TicTacToe":
                    SceneManager.LoadScene("TicTacToe");
                    break;
                case "Taquin":
                    SceneManager.LoadScene("Taquin");
                    break;
                default:
                    break;
            }
        }
        else
        {
            uiScript.PanelNoHeart.SetActive(true);
        }
    }

    // Go back to level map
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Watch rewarded ad with coins multiplier
    public void OnClickWatchAd()
    {
        watchAd.SetActive(false);
        watchAdScript.ShowRewardedVideo("coinsMultiplierRewardedVideo");
    }

    // After the ad the player wins more coins
    public void OnAdFinished()
    {
        GameData.Coins += numberOfCoinsWon * (randomMultiplier - 1);
        textOuputCoinsValue.text = (numberOfCoinsWon * randomMultiplier).ToString();
    }
}
