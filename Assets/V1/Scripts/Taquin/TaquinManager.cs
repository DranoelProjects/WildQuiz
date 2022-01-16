using System;
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
    Level levelData;
    Transform emptyBtn;
    UIScript uiScript;
    GameObject panel;
    Button[] childrens;
    public bool IsGameOver = false;

    private void Awake()
    {
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        panel = GameObject.Find("Panel").gameObject;
        childrens = panel.GetComponentsInChildren<Button>();
        emptyBtn = GameObject.Find("16").gameObject.transform;
        
        levelData = GameDataV2.CurrentLevelData;
        audioSource = gameObject.GetComponent<AudioSource>();
        initGame();
    }

    // When the player win the level
    public void YouWin()
    {
        IsGameOver = true;
        ShowWinningCoins(true);
        panelNextScene.SetActive(true);
        int nextLevel = levelData.Index + 1;
        if (GameDataV2.NextLevel < nextLevel)
        {
            GameDataV2.NextLevel++;
        }
    }

    // If the player buy a wildcard to go directly to next level
    public void BuyGoToNextLevel()
    {
        if (GameDataV2.Coins >= 150)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(sndWin);
            GameDataV2.Coins -= 150;
            GameDataV2.NumberWonLevels++;
            uiScript.UpdateCoins();
            OnClickShowJokersPanel();
            YouWin();
            ShowWinningCoins(false);
        }
        else
        {
            uiScript.PanelNoCoins.SetActive(true);
        }
    }

    // If this the current level for this player display the winning coins
    public void ShowWinningCoins(bool active)
    {
        panelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(active);
    }

    // This function allows us to manage the icon when the user clicks to display the wildcard panel
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
            imageShowJokersPanel.color = new Color(255/255f, 255/255f, 52/255f);
        }
    }

    // Initializes "Taquin" Game
    // From a resolvable instance, perform transformations to initalize the game
    private void initGame()
    {
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

    // Used to initialize the game
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
}
