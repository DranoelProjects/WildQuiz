using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject music;
    PanelUserInfo panelUserInfo;
    SoundManager soundManager;

    //All the about the current lvl is stored here
    public LevelData LevelData;

    private void Awake()
    {
        //removing unnecessary GameManagerScript
        int numGameManager = FindObjectsOfType<GameManagerScript>().Length;
        if (numGameManager != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        //init music
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            Instantiate(music);
        }

        //Caching objects
        panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
        soundManager = GameObject.FindGameObjectWithTag("Music").GetComponent<SoundManager>();

        //Testing if player first connexion
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
        //Initializing callback for active scene changes
        SceneManager.activeSceneChanged += changedActiveScene;

        //Updating user UI
        panelUserInfo.UpdateHearts();
        panelUserInfo.UpdateCoins();
        panelUserInfo.UpdateCurrentLvl();

        //Using player prefs to mute or not the audio
        Mute(PlayerPrefs.GetInt("mute") == 1);
    }

    //This function can be called to mute the all audio sources
    public void Mute(bool val)
    {
        AudioSource[] sources;
        sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource asource in sources)
        {
            asource.mute = val;
        }
    }

    //Callback used in order to stop the music if we are not in levels map
    void changedActiveScene(Scene current, Scene next)
    {
        if (next.buildIndex > 0)
        {
            soundManager.StopMusic();
        } else
        {
            soundManager.PlayMusic();
        }
    }
}
