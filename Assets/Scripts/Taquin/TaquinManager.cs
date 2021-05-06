﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaquinManager : MonoBehaviour
{
    [SerializeField] GameObject panelNextScene, jokersPanel;
    [SerializeField] Image imageShowJokersPanel;
    [SerializeField] Sprite spriteClose, spriteOpen;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWin;
    LevelData levelData;
    GameManagerScript gameManagerScript;
    Transform emptyBtn;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        emptyBtn = GameObject.Find("16").gameObject.transform;
        levelData = gameManagerScript.LevelData;
        audioSource = gameObject.GetComponent<AudioSource>();
        initGame();
    }

    public void YouWin()
    {
        ShowWinningCoins(true);
        panelNextScene.SetActive(true);
        int nextLevel = levelData.Level + 1;
        if (PlayerPrefs.GetInt("NextLevel") < nextLevel)
        {
            PlayerPrefs.SetInt("NextLevel", nextLevel);
        }
    }

    public void BuyGoToNextLevel()
    {
        if (PlayerPrefs.GetInt("CoinsNumber") >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") - 150);
            PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
            PanelUserInfo panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
            panelUserInfo.UpdateCoins();
            OnClickShowJokersPanel();
            YouWin();
            ShowWinningCoins(false);
            panelUserInfo.UpdateCurrentLvl();
        }
        else
        {
            GameObject.Find("GameObjectUI").GetComponent<UIScript>().PanelNoCoins.SetActive(true);
        }
    }

    public void ShowWinningCoins(bool active)
    {
        panelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(active);
    }

    public void OnClickShowJokersPanel()
    {
        jokersPanel.SetActive(!jokersPanel.activeSelf);
        if (jokersPanel.activeSelf)
        {
            imageShowJokersPanel.sprite = spriteClose;
            imageShowJokersPanel.color = Color.red;
        }
        else
        {
            imageShowJokersPanel.sprite = spriteOpen;
            imageShowJokersPanel.color = new Color(255 / 255f, 255 / 255f, 52 / 255f); ;
        }
    }

    private void initGame()
    {
        GameObject panel = GameObject.Find("Panel").gameObject; ;
        Button[] childrens = panel.GetComponentsInChildren<Button>();
        List<Button> allButtons = new List<Button>();
        foreach (Button btn in childrens)
        {
            if (btn.name != "16")
            {
                allButtons.Add(btn);
            }
        }

        int numberOfTransformations = UnityEngine.Random.Range(30, 101);

        for (int i=1; i <= numberOfTransformations; i++)
        {
            foreach (Button btn in allButtons)
            {
                BtnController btnController = btn.GetComponent<BtnController>();
                swapWithEmptyIfPossible(btn);
            }
        }
        foreach (Button btn in allButtons)
        {
            BtnController btnController = btn.GetComponent<BtnController>();
            btnController.InitializingGame = false;
        }
    }

    private void swapWithEmptyIfPossible(Button btn)
    {
        int emptyIndex = emptyBtn.GetSiblingIndex();
        int btnIndex = btn.transform.GetSiblingIndex();
        int difference = emptyIndex - btnIndex;

        switch (difference)
        {
            case 1:
                if (btnIndex != 3 && btnIndex != 7 && btnIndex != 11)
                {
                    btn.transform.SetSiblingIndex(emptyIndex);
                }
                break;
            case 4:
                btn.transform.SetSiblingIndex(emptyIndex);
                emptyBtn.SetSiblingIndex(btnIndex);
                break;
            case -1:
                if (btnIndex != 4 && btnIndex != 8 && btnIndex != 12)
                {
                    btn.transform.SetSiblingIndex(emptyIndex);
                }
                break;
            case -4:
                btn.transform.SetSiblingIndex(emptyIndex);
                emptyBtn.SetSiblingIndex(btnIndex);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        GameData.CurrentNumberOfLevelsBeforeAd++;
    }
}
