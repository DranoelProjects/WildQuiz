using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject music;

    public LevelData LevelData;

    private void Awake()
    {
        int numGameManager = FindObjectsOfType<GameManagerScript>().Length;
        if (numGameManager != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("RewardClaimDatetime")))
        {
            PlayerPrefs.SetString("RewardClaimDatetime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("NextLevel", 85);
            PlayerPrefs.SetInt("HeartsNumber", 20);
            PlayerPrefs.SetInt("CoinsNumber", 1000);
            PlayerPrefs.SetInt("NumberLostLevels", 0);
            PlayerPrefs.SetInt("NumberWonLevels", 0);
            PlayerPrefs.SetInt("mute", 0);
        }
    }
    void Start()
    {
        //init music
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            Instantiate(music);
        }

        SceneManager.activeSceneChanged += changedActiveScene;

        PanelUserInfo panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
        panelUserInfo.UpdateHearts();
        panelUserInfo.UpdateCoins();
        panelUserInfo.UpdateCurrentLvl();

        Mute(PlayerPrefs.GetInt("mute") == 1);
    }

    public void Mute(bool val)
    {
        AudioSource[] sources;
        sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource asource in sources)
        {
            asource.mute = val;
        }
    }

    void changedActiveScene(Scene current, Scene next)
    {
        SoundManager soundManager = GameObject.FindGameObjectWithTag("Music").GetComponent<SoundManager>();
        if (next.buildIndex > 0)
        {
            soundManager.StopMusic();
        } else
        {
            soundManager.PlayMusic();
        }
    }
}
