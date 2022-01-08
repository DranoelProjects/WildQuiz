using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ApiManager apiManager;
    void Awake()
    {
        //removing unnecessary GameManager
        int numGameManager = FindObjectsOfType<GameManager>().Length;
        if (numGameManager != 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(gameObject);

        apiManager = gameObject.GetComponent<ApiManager>();
    }

    public void StartLevel(int levelIndex)
    {
        Debug.Log("Selected level : " + levelIndex);
        apiManager.GetLevel(levelIndex);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("CurrentPage", GameDataV2.CurrentPage);
        PlayerPrefs.SetInt("NextLevel", GameDataV2.NextLevel);
    }
}
