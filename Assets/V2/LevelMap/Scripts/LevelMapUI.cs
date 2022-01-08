using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMapUI : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClickLevelButton()
    {
        gameManager.StartLevel(int.Parse(EventSystem.current.currentSelectedGameObject.name));
    }

}
