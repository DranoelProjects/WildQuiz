using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameManagerScript gameManagerScript;
    private LevelData levelData;
    [SerializeField] Toggle muteToggle;
    [SerializeField] GameObject panelSettings, dailyRewardPanel, btnOverallRanking;
    public GameObject PanelNoHeart, PanelNoCoins, CurrentLevel;
    [SerializeField] Text outputCurrentLevel;

    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        levelData = gameManagerScript.LevelData;
        if (SceneManager.GetActiveScene().name == "LevelMap")
        {
            CurrentLevel.SetActive(false);
            dailyRewardPanel.SetActive(true);
        } else
        {
            CurrentLevel.SetActive(true);
            dailyRewardPanel.SetActive(false);
            btnOverallRanking.SetActive(false);
            outputCurrentLevel.text = levelData.Level.ToString();
        }
    }

    public void OnClickShowSettingsPanel()
    {
        panelSettings.SetActive(!panelSettings.activeSelf);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        muteToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt("mute") == 0);
    }

    public void OnClickMuteToggle()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        audioSource = GetComponent<AudioSource>();
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
        GameObject.Find("GameManager").GetComponent<WatchAd>().ShowRewardedVideo("rewardedVideo");
    }
}
