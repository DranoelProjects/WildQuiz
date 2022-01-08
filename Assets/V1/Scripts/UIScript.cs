using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameManagerScript gameManagerScript;
    WatchAd watchAd;
    private LevelData levelData;
    [SerializeField] Toggle muteToggle;
    [SerializeField] GameObject panelSettings, dailyRewardPanel, btnOverallRanking;
    public GameObject PanelNoHeart, PanelNoCoins, CurrentLevel;
    [SerializeField] Text outputCurrentLevel;

    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    private void Awake() {
        GameObject gameManager = GameObject.Find("GameManager");
        audioSource = GetComponent<AudioSource>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        watchAd = GameObject.Find("GameManager").GetComponent<WatchAd>();
    }

    private void Start()
    {
        levelData = gameManagerScript.LevelData;
        // Disabling or not objects of the GameObjectUI
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

    // On click top right button to open/close settings
    public void OnClickShowSettingsPanel()
    {
        panelSettings.SetActive(!panelSettings.activeSelf);
        OnClickPlaySound();
        muteToggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt("mute") == 0);
    }

    // Toggle used to mute or unmute the game
    public void OnClickMuteToggle()
    {
        OnClickPlaySound();
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

    // Play on click btn sound
    public void OnClickPlaySound(){
        audioSource.PlayOneShot(sndClick);
    }

    // Show rewarded video
    // The reward is one heart
    public void ShowRewardedVideo()
    {
        watchAd.ShowRewardedVideo("rewardedVideo");
    }
}
