using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] GameObject winningCoins;
    [SerializeField] Button nextLvlButton;
    GameManagerScript gameManagerScript;

    public void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        if (PlayerPrefs.GetInt("NextLevel") == 86 && gameManagerScript.LevelData.Level == 85)
        {
            nextLvlButton.interactable = false;
        }
    }
    public void ActiveWinningCoins(bool active)
    {
        winningCoins.SetActive(active);
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
}
