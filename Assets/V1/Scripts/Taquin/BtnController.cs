using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour {
    Transform emptyBtn;
    GameObject panel;
    Button[] allButtons;
    TaquinManager taquinManager;
    public bool InitializingGame = true;

    private void Awake()
    {
        emptyBtn = GameObject.Find("16").gameObject.transform;
        panel = GameObject.Find("Panel").gameObject;
        taquinManager = GameObject.Find("Canvas").GetComponent<TaquinManager>();
    }

    // If possible swap the current button with the empty button
    public void OnClickSwapWithEmpty()
    {
            if(!taquinManager.IsGameOver){
            int emptyIndex = emptyBtn.GetSiblingIndex();
            int btnIndex = gameObject.transform.GetSiblingIndex();
            int difference = emptyIndex - btnIndex;

            switch (difference)
            {
                case 1:
                    if(btnIndex != 3 && btnIndex != 7 && btnIndex != 11)
                    {
                        gameObject.transform.SetSiblingIndex(emptyIndex);
                    }
                    break;
                case 4:
                    gameObject.transform.SetSiblingIndex(emptyIndex);
                    emptyBtn.SetSiblingIndex(btnIndex);
                    break;
                case -1:
                    if(btnIndex != 4 && btnIndex != 8 && btnIndex != 12)
                    {
                        gameObject.transform.SetSiblingIndex(emptyIndex);
                    }
                    break;
                case -4:
                    gameObject.transform.SetSiblingIndex(emptyIndex);
                    emptyBtn.SetSiblingIndex(btnIndex);
                    break;
                default:
                    break;
            }
            if(!InitializingGame)
                checkIfPlayerWon();
        }
    }

    // After swaping the two buttons check if the "taquin" is solved
    void checkIfPlayerWon()
    {
        allButtons = panel.GetComponentsInChildren<Button>();
        int rightPos = 0;

        foreach (Button btn in allButtons)
        {
            if(int.Parse(btn.name) == (btn.transform.GetSiblingIndex() + 1))
            {
                rightPos++;
            }
        }
        if(rightPos == 16)
        {
            taquinManager.YouWin();
        }
    }
}
