using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameManagerScript gameManagerScript;
    [SerializeField] Toggle muteToggle;
    [SerializeField] GameObject panelSettings;
    public GameObject PanelNoHeart, PanelNoCoins;

    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void OnClickShowSettingsPanel()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        bool isSettingsPanelActive = !panelSettings.activeInHierarchy;
        panelSettings.SetActive(isSettingsPanelActive);
        muteToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt("mute") == 0);
    }


    public void OnClickMuteToggle()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        if (muteToggle.isOn)
        {
            PlayerPrefs.SetInt("mute", 0);
            gameManagerScript.Mute(false);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
            gameManagerScript.Mute(true);
        }
    }

    public void ShowRewardedVideo()
    {
        GameObject.Find("GameManager").GetComponent<WatchAd>().ShowRewardedVideo();
    }
}
