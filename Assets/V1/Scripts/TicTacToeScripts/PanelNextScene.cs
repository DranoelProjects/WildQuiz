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

    // This script is like the NextSceneScript but there is a new case for TicTacToe with tie
    // It's not really justified to have 2 scripts for that
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
