using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("NextLevel"))
        {
            PlayerPrefs.SetInt("NextLevel", 1);
        }
        if (PlayerPrefs.GetInt("NextLevel") < int.Parse(gameObject.name))
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Image>().color = Color.grey;

            if(int.Parse(gameObject.name) % 25 == 0)
            {
                gameObject.GetComponent<Image>().color = Color.red;
            }

            if (int.Parse(gameObject.name) % 4 == 0)
            {
                gameObject.GetComponent<Image>().color = Color.blue;
            }
        }
    }
    public void StartSceneLevel(LevelData levelData)
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        if (PlayerPrefs.GetInt("HeartsNumber") > 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().LevelData = levelData;
            switch (levelData.Type)
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
            if (!GameObject.FindGameObjectWithTag("Alert"))
            {         
                GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoHeart.SetActive(true);
            }
        }
    }
}
