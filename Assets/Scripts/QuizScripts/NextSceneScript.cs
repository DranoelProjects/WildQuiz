using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    [SerializeField] GameObject winningCoins;
    public void ActiveWinningCoins(bool active)
    {
        winningCoins.SetActive(active);
    }

    public void StartNextSceneLevel()
    {
        GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
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
