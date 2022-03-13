using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionTicTacToe : MonoBehaviour
{
    GameManagerTicTacToe gm;

    private void Start()
    {
        gm = GameObject.Find("GameManagerTicTacToe").GetComponent<GameManagerTicTacToe>();
    }

    // If the player clicks on a button write an X on it
    public void WriteX()
    {
        if(!gm.IsGameOver){
            Level levelData = GameDataV2.CurrentLevelData;
            GetComponentInChildren<Text>().text = "X";
            GetComponent<Button>().interactable = false;
            gm.Tab[int.Parse(gameObject.name)] = "X";
            if (gm.IsWinner("X"))
            {
                gm.IsGameOver = true;
                gm.PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(false);
                gm.PlayWinningSound();
                if (levelData.Index == GameDataV2.NextLevel)
                {
                    gm.PanelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(true);
                    GameDataV2.NumberWonLevels++;
                    GameDataV2.Coins += 10;
                }
                gm.PanelNextScene.GetComponentInChildren<PanelNextScene>().GoToNextLevel = 1;
                gm.PanelNextScene.SetActive(true);

                int nextLevel = levelData.Index + 1;
                if (GameDataV2.NextLevel < nextLevel)
                {
                    GameDataV2.NextLevel = nextLevel;
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
