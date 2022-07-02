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
    [SerializeField] GameObject panelSettings, dailyRewardPanel, btnOverallRanking, panelHandleRequest;
    public GameObject PanelNoHeart, PanelNoCoins;
    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    [Header("Handle request panel")]
    [SerializeField] Sprite errorSprite;
    [SerializeField] Sprite loadingSprite;
    [SerializeField] Image loadingImage;
    private bool loading;

    [Header("Panel User Info")]
    [SerializeField] Text heartsNumber;
    [SerializeField] Text coinsNumber;
    [SerializeField] Text nextLevelNumber;
    [SerializeField] Image heart;

    [Header("Panel Settings")]
    [SerializeField] Text currentLevelNumber;
    [SerializeField] Text lostLevelsNumber;
    [SerializeField] Text wonLevelsNumber;
    [SerializeField] GameObject gameObjectStats, textCredits;
    [SerializeField] Button btnShowStats, btnShowCredits;
    bool isShowingStats = false, isShowingsCredits = false;

    WatchAd watchAd;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        watchAd = GameObject.Find("GameManager").GetComponent<WatchAd>();
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
        UpdateNextLevel();
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
        if(coinsNumber)
            coinsNumber.text = GameDataV2.Coins.ToString();
    }

    public void UpdateNextLevel()
    {
        nextLevelNumber.text = GameDataV2.NextLevel.ToString();
    }

    // Display statistics 
    public void OnClickShowStats()
    {
        audioSource.PlayOneShot(sndClick);
        if (!isShowingStats)
        {
            btnShowStats.GetComponent<Image>().color = new Color(255 / 255f, 246 / 255f, 0 / 255f);
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
            btnShowCredits.GetComponent<Image>().color = new Color(255 / 255f, 246 / 255f, 0 / 255f);
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

    // Show loading panel
    public void SetLoadingPanelVisibility(bool userAction)
    {
        bool isHandleRequestPanelActive = !panelHandleRequest.gameObject.activeInHierarchy;
        if (!loading)
        {
            panelHandleRequest.gameObject.SetActive(isHandleRequestPanelActive);
            Scene scene = SceneManager.GetActiveScene();
            if(scene.buildIndex > 0 && userAction)
                SceneManager.LoadScene(0);
        }
        if (isHandleRequestPanelActive)
            handleRequestLoading();
    }

    public void HandleRequestError(string error)
    {
        loading = false;
        Text text = panelHandleRequest.GetComponentInChildren<Text>();
        text.text = "Une erreur est survenue, veuillez réessayer plus tard (vérifiez votre connexion internet) :\n" + error;
        loadingImage.sprite = errorSprite;
    }

    void handleRequestLoading()
    {
        loading = true;
        Text text = panelHandleRequest.GetComponentInChildren<Text>();
        text.text = "Chargement...";
        loadingImage.sprite = loadingSprite;
    }

    //Reward : Heart
    public void OnClickShowRewardedVideo()
    {
        watchAd.ShowRewardedVideo("rewardedVideo");
    }
}
