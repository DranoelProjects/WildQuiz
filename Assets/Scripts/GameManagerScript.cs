using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject music;

    public LevelData LevelData;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        int numGameManager = FindObjectsOfType<GameManagerScript>().Length;
        if (numGameManager != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        } 
    }
    void Start()
    {
        //init hearts and coins
        if (PlayerPrefs.GetInt("FirstConnexion") != 1)
        {
            PlayerPrefs.SetInt("FirstConnexion", 1);
            PlayerPrefs.SetInt("NextLevel", 1);
            PlayerPrefs.SetInt("HeartsNumber", 20);
            PlayerPrefs.SetInt("CoinsNumber", 1000);
            PlayerPrefs.SetInt("NumberLostLevels", 0);
            PlayerPrefs.SetInt("NumberWonLevels", 0);
            PlayerPrefs.SetInt("mute", 0);
        }

        //init music
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            Instantiate(music);
        }

        PanelUserInfo panelUserInfo = GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>();
        panelUserInfo.UpdateHearts();
        panelUserInfo.UpdateCoins();

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
}
