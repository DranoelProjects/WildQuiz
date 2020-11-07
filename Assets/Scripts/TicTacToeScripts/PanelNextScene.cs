using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PanelNextScene : MonoBehaviour
{
    [SerializeField] Button btnNextLevel;
    Text result;
    public int GoToNextLevel = 0;

    private void Start()
    {
        result = GetComponentInChildren<Text>();
        if (GoToNextLevel == 1)
        {
            btnNextLevel.GetComponentInChildren<Text>().text = "Niveau suivant";
            result.text = "Victoire !";
        }
        else if (GoToNextLevel == 2)
        {
            result.text = "Défaite !";
            btnNextLevel.GetComponentInChildren<Text>().text = "Rejouer";
        } else
        {
            result.text = "Egalité !";
            btnNextLevel.GetComponentInChildren<Text>().text = "Rejouer";
        }
    }


    public void NextLevel()
    {
        GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        LevelData levelData = gameManagerScript.LevelData;
        LevelData nextLevelData = levelData.NextLevelData;
        if (PlayerPrefs.GetInt("HeartsNumber") > 0)
        {
            if (GoToNextLevel == 1)
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
