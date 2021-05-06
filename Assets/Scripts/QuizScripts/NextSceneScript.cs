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
    int randomMultiplier = 0;
    int numberOfCoinsWon = 10;

    public void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        if (PlayerPrefs.GetInt("NextLevel") == 86 && gameManagerScript.LevelData.Level == 85)
        {
            nextLvlButton.interactable = false;
        }
    }
    public void ActiveWinningCoins(bool active, int value= 10)
    {
        textOuputCoinsValue.text = "+ " + value.ToString();
        winningCoins.SetActive(active);
        numberOfCoinsWon = value;
        randomMultiplier = Random.Range(2, 5);
        textMultiplier.text = randomMultiplier.ToString();
    }

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
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoHeart.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickWatchAd()
    {
        watchAd.SetActive(false);
        GameObject.Find("GameManager").GetComponent<WatchAd>().ShowRewardedVideo("coinsMultiplierRewardedVideo");
    }

    public void OnAdFinished()
    {
        GameData.Coins += numberOfCoinsWon * (randomMultiplier - 1);
        textOuputCoinsValue.text = (numberOfCoinsWon * randomMultiplier).ToString();
    }
}
