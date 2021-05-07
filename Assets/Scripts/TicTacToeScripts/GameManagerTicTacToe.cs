using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerTicTacToe : MonoBehaviour
{
    public string[] Tab = new string[9];
    public GameObject PanelNextScene;
    [SerializeField] GameObject jokersPanel, helpPanel;


    [SerializeField] Image imageShowJokersPanel;
    [SerializeField] Sprite spriteClose, spriteOpen;
    GameManagerScript gameManagerScript;
    PanelUserInfo panelUserInfo;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWin, sndLoose;

    AI ai = new AI();

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
    }
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        int turn = Random.Range(0, 2);
        if (turn == 0) ComputerPlay();
    }
    public void ComputerPlay()
    {
        int button = ai.BestPosition(gameManagerScript.LevelData.Difficulty);
        Button btn = GameObject.Find(button.ToString()).GetComponent<Button>();
        btn.interactable = false;
        btn.GetComponentInChildren<Text>().text = "O";
        Tab[button] = "O";

        if (IsWinner("O"))
        {
            PlayLosingSound();
            if (gameManagerScript.LevelData.Level == PlayerPrefs.GetInt("NextLevel"))
            {
                PlayerPrefs.SetInt("NumberLostLevels", PlayerPrefs.GetInt("NumberLostLevels") + 1);
            }
            PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
            PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 2;
            PlayerPrefs.SetInt("HeartsNumber", PlayerPrefs.GetInt("HeartsNumber") - 1);
            panelUserInfo.UpdateHearts();
            PanelNextScene.SetActive(true);
            return;
        }

        if (ArrayIsFull())
        {
            PanelNextScene.SetActive(true);
            PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 0;
            PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
            ColorBlue();
            return;
        }
    }

    public bool IsWinner(string p)
    {
        if(Tab[0] == p && Tab[1] == p && Tab[2] == p)
        {
            colorRed(0, 1, 2);
            return true;
        }
        if (Tab[3] == p && Tab[4] == p && Tab[5] == p)
        {
            colorRed(3, 4, 5);
            return true;
        }
        if (Tab[6] == p && Tab[7] == p && Tab[8] == p)
        {
            colorRed(6, 7, 8);
            return true;
        }

        if (Tab[0] == p && Tab[3] == p && Tab[6] == p)
        {
            colorRed(0, 3, 6);
            return true;
        }
        if (Tab[1] == p && Tab[4] == p && Tab[7] == p)
        {
            colorRed(1, 4, 7);
            return true;
        }
        if (Tab[2] == p && Tab[5] == p && Tab[8] == p)
        {
            colorRed(2, 5, 8);
            return true;
        }

        if (Tab[0] == p && Tab[4] == p && Tab[8] == p)
        {
            colorRed(0, 4, 8);
            return true;
        }
        if (Tab[2] == p && Tab[4] == p && Tab[6] == p)
        {
            colorRed(2, 4, 6);
            return true;
        }
        return false;
    }

    public bool ArrayIsFull()
    {
        for (int i=0; i<Tab.Length; i++)
        {
            if (Tab[i] == string.Empty) return false;
        }

        return true;
    }

    private void colorRed(int c1, int c2, int c3)
    {
        GameObject.Find(c1.ToString()).GetComponent<Button>().GetComponent<Image>().color = Color.yellow;
        GameObject.Find(c2.ToString()).GetComponent<Button>().GetComponent<Image>().color = Color.yellow;
        GameObject.Find(c3.ToString()).GetComponent<Button>().GetComponent<Image>().color = Color.yellow;
    }

    public void ColorBlue()
    {
        for (int i=0; i < Tab.Length; i++)
        {
            GameObject.Find(i.ToString()).GetComponent<Button>().GetComponent<Image>().color = Color.blue;
        }
    }

    public void OnClickShowJokersPanel()
    {
        jokersPanel.SetActive(!jokersPanel.activeSelf);

        if (jokersPanel.activeInHierarchy)
        {
            imageShowJokersPanel.sprite = spriteClose;
            imageShowJokersPanel.color = Color.red;
        }
        else
        {
            imageShowJokersPanel.sprite = spriteOpen;
            imageShowJokersPanel.color = Color.white;
        }
    }

    public void OnClickShowHelpPanel()
    {
        bool isHelpPanelActive = !helpPanel.gameObject.activeInHierarchy;
        helpPanel.gameObject.SetActive(isHelpPanelActive);
    }

    public void BuyGoToNextLevel()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 150);
            PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
            panelUserInfo.UpdateCoins();
            PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
            OnClickShowJokersPanel();
            PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 1;
            PanelNextScene.SetActive(true);
            int nextLevel = gameManagerScript.LevelData.Level + 1;
            if (PlayerPrefs.GetInt("NextLevel") < nextLevel)
            {
                PlayerPrefs.SetInt("NextLevel", nextLevel);
            }
            panelUserInfo.UpdateCurrentLvl();
        } else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }
    }

    public void PlayWinningSound()
    {
        audioSource.PlayOneShot(sndWin);
    }

    public void PlayLosingSound()
    {
        audioSource.PlayOneShot(sndLoose);
    }
    private void OnDestroy()
    {
        GameData.CurrentNumberOfLevelsBeforeAd++;
    }
}
