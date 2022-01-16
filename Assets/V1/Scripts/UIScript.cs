using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    GameManager gameManager;
    private Level levelData;
    [SerializeField] Toggle muteToggle;
    [SerializeField] GameObject panelSettings, dailyRewardPanel, btnOverallRanking;
    public GameObject PanelNoHeart, PanelNoCoins;

    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    [Header("Panel User Info")]
    [SerializeField] Text heartsNumber;
    [SerializeField] Text coinsNumber;
    [SerializeField] Image heart;

    [Header("Panel Settings")]
    [SerializeField] Text currentLevelNumber;
    [SerializeField] Text lostLevelsNumber;
    [SerializeField] Text wonLevelsNumber;
    [SerializeField] GameObject gameObjectStats, textCredits, helpPanel;
    [SerializeField] Button btnShowStats, btnShowCredits;
    bool isShowingStats = false, isShowingsCredits = false;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        levelData = GameDataV2.CurrentLevelData;
        // Disabling or not objects of the GameObjectUI
        if (SceneManager.GetActiveScene().name == "LevelMap")
        {
            dailyRewardPanel.SetActive(true);
        } else
        {
            dailyRewardPanel.SetActive(false);
            btnOverallRanking.SetActive(false);
        }

        // Init
        UpdateCoins();
        UpdateHearts();
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
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        OnClickPlaySound();
        if (muteToggle.isOn)
        {
            PlayerPrefs.SetInt("mute", 0);
            gameManager.Mute(false);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
            gameManager.Mute(true);
        }
    }

    // Play on click btn sound
    public void OnClickPlaySound(){
        audioSource.PlayOneShot(sndClick);
    }

    // Can be called to update the hearts number
    public void UpdateHearts()
    {
        heartsNumber.text = GameDataV2.Hearts.ToString();

        if (GameDataV2.Hearts != 0)
        {
            heart.color = new Color(248 / 255f, 60 / 255f, 60 / 255f);
        }
        else
        {
            heart.color = Color.grey;
        }
    }

    // Can be called to update the coins number
    public void UpdateCoins()
    {
        coinsNumber.text = GameDataV2.Coins.ToString();
    }

    // Display statistics 
    public void OnClickShowStats()
    {
        audioSource.PlayOneShot(sndClick);
        if (!isShowingStats)
        {
            btnShowStats.GetComponent<Image>().color = Color.green;
            gameObjectStats.SetActive(true);
            currentLevelNumber.text = GameDataV2.NextLevel.ToString();
            lostLevelsNumber.text = GameDataV2.NumberLostLevels.ToString();
            wonLevelsNumber.text = GameDataV2.NumberWonLevels.ToString();
            isShowingsCredits = true;
            OnClickShowCredits();
        }
        else
        {
            btnShowStats.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            gameObjectStats.SetActive(false);
        }
        isShowingStats = !isShowingStats;
    }

    // Display credits
    public void OnClickShowCredits()
    {
        audioSource.PlayOneShot(sndClick);
        if (!isShowingsCredits)
        {
            btnShowCredits.GetComponent<Image>().color = Color.green;
            textCredits.SetActive(true);
            isShowingStats = true;
            OnClickShowStats();
        }
        else
        {
            btnShowCredits.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            textCredits.SetActive(false);
        }
        isShowingsCredits = !isShowingsCredits;
    }

    // Help
    public void OnClickShowHelpPanel()
    {
        audioSource.PlayOneShot(sndClick);
        bool isHelpPanelActive = !helpPanel.gameObject.activeInHierarchy;
        helpPanel.gameObject.SetActive(isHelpPanelActive);
    }
}
