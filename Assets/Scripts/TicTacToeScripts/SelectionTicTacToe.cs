﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionTicTacToe : MonoBehaviour
{
    GameManagerTicTacToe gm;
    GameManagerScript gameManagerScript;
    PanelUserInfo panelUserInfo;

    private void Awake() {
        panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>(); 
    }
    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        gm = GameObject.Find("GameManagerTicTacToe").GetComponent<GameManagerTicTacToe>();
    }

    // If the player clicks on a button write an X on it
    public void WriteX()
    {
        if(!gm.IsGameOver){
            GetComponentInChildren<Text>().text = "X";
            GetComponent<Button>().interactable = false;
            gm.Tab[int.Parse(gameObject.name)] = "X";
            if (gm.IsWinner("X"))
            {
                gm.IsGameOver = true;
                gm.PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
                gm.PlayWinningSound();
                if (gameManagerScript.LevelData.Level == PlayerPrefs.GetInt("NextLevel"))
                {
                    gm.PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(true);
                    PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
                    PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") + 10);
                    panelUserInfo.UpdateCoins();
                }
                gm.PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 1;
                gm.PanelNextScene.SetActive(true);
                int nextLevel = gameManagerScript.LevelData.Level + 1;
                if (PlayerPrefs.GetInt("NextLevel") < nextLevel)
                {
                    PlayerPrefs.SetInt("NextLevel", nextLevel);
                }
            }
            else if (gm.ArrayIsFull())
            {
                gm.IsGameOver = true;
                gm.ColorBlue();
                gm.PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 0;
                gm.PanelNextScene.SetActive(true);
                gm.PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
            }
            else
            {
                gm.ComputerPlay();
            }
        }
    }
}
