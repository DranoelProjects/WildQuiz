using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;

public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject jokersPanel, cluePanel, panelAnswersButtons, panelInputAnswer, watchAdGameObject;

    Button[] allButtons;

    [SerializeField] Image imageShowJokersPanel;
    [SerializeField] Sprite spriteClose, spriteOpen, spriteGoodAnswer, spriteWrongAnswer;
    [SerializeField] Button btnOneWrong, btnTwoWrongs, btnBuyClue, btnCheckAnswerInDirectMode;

    [Header("Load quiz data")]
    public Level levelData;
    [SerializeField] GameObject gameObjectQuestion;
    [SerializeField] Text questionText, answer1, answer2, answer3, answer4, clue, theme;
    [SerializeField] Image imageWithQuestion;

    [Header("Timer")]
    float timeLeft = 40.0f;
    bool isTimerActivate = true;

    UIScript uiScript;
    [SerializeField] Text textTimer, textTimer2;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWrong, sndWin, sndBtn;

    [Header("Answer mode")]
    [SerializeField] GameObject panelAnswerMode;
    bool isDirectlyAnswerMode = false;

    [Header("InputQuiz")]
    [SerializeField] ListenSound listenSound;
    [SerializeField] GameObject textLevel, btnListen, gameObjectAnswerWas;
    [SerializeField] InputField answerInput;
    [SerializeField] Text textAnswerWas;

    [Header("ResultPanel")]
    int randomMultiplier = 0;
    int numberOfCoinsWon = 10;
    [SerializeField] Button nextLvlButton;
    [SerializeField] GameObject resultPanel, winningCoins, resultImage;
    [SerializeField] Text textOuputCoinsValue, textMultiplier, result, info;
    [SerializeField] Sprite sadCat;

    private GameManager gameManager;
    private WatchAd watchAd;

    private void Awake()
    {
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        levelData = GameDataV2.CurrentLevelData;
        audioSource = gameObject.GetComponent<AudioSource>();
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        watchAd = gameManagerObject.GetComponent<WatchAd>();
    }

    private void Start()
    {
        allButtons = GameObject.Find("PanelAnswersButtons").GetComponentsInChildren<Button>();
        loadLevelData();

        if (GameDataV2.NextLevel == 86 && GameDataV2.CurrentLevelData.Index == 85)
        {
            nextLvlButton.interactable = false;
        }
    }

    void Update()
    {
        // Timer of the level
        if (isTimerActivate)
        {
            timeLeft -= Time.deltaTime;
            textTimer.text = (timeLeft).ToString("0");
            textTimer2.text = (timeLeft).ToString("0");
            if (timeLeft < 0)
            {
                isTimerActivate = false;
                audioSource.Stop();
                ShowWinningCoins(false);
                if (levelData.Index == GameDataV2.NextLevel)
                {
                    GameDataV2.NumberLostLevels++;
                }
                audioSource.PlayOneShot(sndWrong);
                GameDataV2.Hearts--;
                uiScript.UpdateHearts();

                WriteResult("Temps écoulé !");
                RevealAnswer(false, null, false);
            }
        }
        else
        {
            timeLeft = 0.0f;
            textTimer.text = (timeLeft).ToString("0");
            textTimer2.text = (timeLeft).ToString("0");
        }
    }

    // Loads the data of the level according to type
    void loadLevelData()
    {
        gameObjectAnswerWas.SetActive(false);
        theme.text = levelData.Theme;
        if (levelData.Type == "Quiz")
        {
            panelAnswersButtons.SetActive(true);
            panelInputAnswer.SetActive(false);
            textLevel.SetActive(false);
            btnListen.SetActive(false);
            questionText.text = levelData.Question;
            answer1.text = levelData.Answer1;
            answer1.fontSize = levelData.FontSizeBtnAnswers;
            answer2.text = levelData.Answer2;
            answer2.fontSize = levelData.FontSizeBtnAnswers;
            answer3.text = levelData.Answer3;
            answer3.fontSize = levelData.FontSizeBtnAnswers;
            answer4.text = levelData.Answer4;
            answer4.fontSize = levelData.FontSizeBtnAnswers;
            imageWithQuestion.sprite = levelData.SpriteWithQuestion;
            imageWithQuestion.GetComponent<RectTransform>().localScale = new Vector3(levelData.ImageScale, levelData.ImageScale, levelData.ImageScale);
        }
        else if (levelData.Type == "InputQuiz")
        {
            panelAnswerMode.SetActive(false);
            gameObjectQuestion.SetActive(false);
            panelAnswersButtons.SetActive(false);
            panelInputAnswer.SetActive(true);
            btnOneWrong.gameObject.SetActive(false);
            btnTwoWrongs.gameObject.SetActive(false);

            if (levelData.NeedText)
            {
                textLevel.SetActive(true);
                textLevel.GetComponent<Text>().text = levelData.TextLevel;
            }
            else
            {
                textLevel.SetActive(false);
            }
            if (levelData.NeedImage)
            {
                imageWithQuestion.sprite = levelData.SpriteWithQuestion;
                imageWithQuestion.GetComponent<RectTransform>().localScale = new Vector3(levelData.ImageScale, levelData.ImageScale, levelData.ImageScale);
            }
            else
            {
                imageWithQuestion.enabled = false;
            }
            if (levelData.NeedBtnListen)
            {
                btnListen.SetActive(true);
                listenSound.Sound = levelData.Sound;
            }
            else
            {
                btnListen.SetActive(false);
            }
        }
        clue.text = levelData.Clue;
    }

    // Reveals selected btn
    public void RevealAnswer(bool isRightAnswer, GameObject selectedButton, bool isJoker)
    {
        isTimerActivate = false;
        if (levelData.Type == "Quiz")
        {
            if (isDirectlyAnswerMode)
            {
                if (isRightAnswer)
                {
                    setQuizResultPanelForRightAnswer(null);
                } else
                {
                    setQuizResultPanelForWrongAnswer(null);
                }
            } else
            {
                if ((selectedButton != null) && isRightAnswer)
                {
                    setQuizResultPanelForRightAnswer(selectedButton);
                }
                else if (selectedButton != null)
                {
                    setQuizResultPanelForWrongAnswer(selectedButton);
                }
                foreach (Button btn in allButtons)
                {
                    btn.GetComponent<Button>().interactable = false;
                    string btnAnswer = btn.GetComponentInChildren<Text>().text;
                    if (isJoker && (btnAnswer == GameDataV2.CurrentLevelData.rightAnswer))
                    {
                        btn.GetComponent<Image>().sprite = spriteGoodAnswer;
                        selectedButton.GetComponent<Image>().color = Color.white;
                        resultPanel.GetComponent<Image>().color = new Color(21 / 255f, 89 / 255f, 9 / 255f);
                    }
                }
            }
        } else
        {
            if (isRightAnswer)
            {
                resultPanel.GetComponent<Image>().color = new Color(21 / 255f, 89 / 255f, 9 / 255f);
            } else
            {
                resultPanel.GetComponent<Image>().color = Color.red;
                resultImage.GetComponent<Image>().sprite = sadCat;
                resultImage.GetComponent<Animator>().enabled = false;
            }
        }
        int nextLevel = levelData.Index + 1;
        if (GameDataV2.NextLevel < nextLevel)
        {
            GameDataV2.NextLevel = nextLevel;
        }
        info.text = levelData.Info;
        textAnswerWas.text = levelData.RightAnswer;
        textAnswerWas.color = Color.green;
        StartCoroutine(nextScene());
    }

    // Wait 1 sec then display the following scene panel
    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1f);
        resultPanel.SetActive(true);
        gameObjectAnswerWas.SetActive(true);
    }

    // show result panel with right answer theme
    private void setQuizResultPanelForRightAnswer(GameObject selectedButton)
    {
        if (!isDirectlyAnswerMode)
        {
            selectedButton.GetComponent<Image>().sprite = spriteGoodAnswer;
            selectedButton.GetComponent<Image>().color = Color.white;
        }
        resultPanel.GetComponent<Image>().color = new Color(21 / 255f, 89 / 255f, 9 / 255f);
    }

    // show result panel with wrong answer theme
    private void setQuizResultPanelForWrongAnswer(GameObject selectedButton)
    {
        if (!isDirectlyAnswerMode)
        {
            selectedButton.GetComponent<Image>().sprite = spriteWrongAnswer;
            selectedButton.GetComponent<Image>().color = Color.white;
        }
        resultPanel.GetComponent<Image>().color = Color.red;
        resultImage.GetComponent<Image>().sprite = sadCat;
        resultImage.GetComponent<Animator>().enabled = false;
    }

    // Reveals X wrong answers
    public void DeleteWrongAnswers(int x)
    {
        foreach (Button btn in allButtons)
        {
            string btnAnswer = btn.GetComponentInChildren<Text>().text;
            if (levelData.RightAnswer != btnAnswer && x > 0)
            {
                x--;
                btn.GetComponent<Image>().color = Color.red;
                btn.interactable = false;
            }
        }
    }

    // Winning coins of the following scene 
    public void ShowWinningCoins(bool active, int value = 10)
    {
        activeWinningCoins(active, value);
    }

    // Display the result on the panel next scene 
    public void WriteResult(string text)
    {
        result.text = text;
    }

    // This function allows us to manage the icon when the user clicks to display the wildcard panel
    public void OnClickShowJokersPanel()
    {
        jokersPanel.SetActive(!jokersPanel.activeSelf);
        if (jokersPanel.activeSelf)
        {
            imageShowJokersPanel.sprite = spriteClose;
            imageShowJokersPanel.color = Color.red;
        }
        else
        {
            imageShowJokersPanel.sprite = spriteOpen;
            imageShowJokersPanel.color = new Color(255 / 255f, 255 / 255f, 52 / 255f);
        }
    }

    // The user can't buy a wildcard again so it disables these buttons 
    public void DisableBuyButton()
    {
        btnOneWrong.interactable = false;
        btnOneWrong.GetComponent<Image>().color = Color.black;
        btnOneWrong.GetComponentInChildren<Text>().text = "Achat impossible";
        btnTwoWrongs.interactable = false;
        btnTwoWrongs.GetComponent<Image>().color = Color.black;
        btnTwoWrongs.GetComponentInChildren<Text>().text = "Achat impossible";
        OnClickShowJokersPanel();
    }

    // Activation or deactivation of the clue panel
    public void ShowCluePanel()
    {
        bool isCluePanelActive = !cluePanel.gameObject.activeInHierarchy;
        cluePanel.gameObject.SetActive(isCluePanelActive);

        if (cluePanel.gameObject.activeInHierarchy)
        {
            btnBuyClue.interactable = false;
            btnBuyClue.GetComponent<Image>().color = Color.black;
            btnBuyClue.GetComponentInChildren<Text>().text = "Achat impossible";
        }
    }

    // For input quiz or directly mode, get the answer
    public void OnClickCheckInput()
    {
        InputField inputField = answerInput.GetComponent<InputField>();
        if(inputField.text == "" || inputField.text == null)
        {
            inputField.GetComponentInChildren<Text>().text = "Réponse vide";
            inputField.GetComponentInChildren<Text>().color = Color.red;
        } else
        {
            btnCheckAnswerInDirectMode.interactable = false;
            CheckAnswer(inputField.text, null);
        }
    }

    // For input quiz or directly mode, check the answer
    public void CheckAnswer(string playerAnswer, GameObject selectedButton)
    {
        bool isRightAnswer = false;
        audioSource.Stop();
        ShowWinningCoins(false);
        double threshold = 0.8;
        bool isThemeEqualToSpelling = levelData.Theme.Equals("Orthographe");
        if (!isThemeEqualToSpelling && (
            (Levenshtein.ComputeCorrelation(levelData.RightAnswer, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer1, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer2, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer3, playerAnswer, false) > threshold)
            ) || (isThemeEqualToSpelling && (
                (levelData.RightAnswer.Equals(playerAnswer))
                || (levelData.OtherAcceptedAnswer1.Equals(playerAnswer))
                || (levelData.OtherAcceptedAnswer2.Equals(playerAnswer))
                || (levelData.OtherAcceptedAnswer3.Equals(playerAnswer))
                )))
        {
            isRightAnswer = true;
            audioSource.PlayOneShot(sndWin);
            if (levelData.Index == GameDataV2.NextLevel)
            {
                GameDataV2.NumberWonLevels++;
                if (isDirectlyAnswerMode)
                {
                    ShowWinningCoins(true, 30);
                    GameDataV2.Coins += 30;
                }
                else
                {
                    ShowWinningCoins(true);
                    GameDataV2.Coins += 10;
                }
                uiScript.UpdateCoins();
            }
            WriteResult("Bonne réponse !");
        }
        else
        {
            if (levelData.Index == GameDataV2.NextLevel)
            {
                GameDataV2.NumberLostLevels++;
            }
            else
            {
                audioSource.PlayOneShot(sndWrong);
            }
            GameDataV2.Hearts--;
            uiScript.UpdateHearts();
            WriteResult("Mauvaise réponse !");
        }
        RevealAnswer(isRightAnswer, selectedButton, false);
    }

    // Choose between 4 answers mode
    public void OnClickBtnProposals()
    {
        OnClickPlayBtnSound();
        isDirectlyAnswerMode = false;
        panelAnswerMode.SetActive(false);
    }

    // Answer directly mode with input field
    public void OnClickBtnDirectly()
    {
        OnClickPlayBtnSound();
        isDirectlyAnswerMode = true;
        panelAnswerMode.SetActive(false);
        panelAnswersButtons.SetActive(false);
        panelInputAnswer.SetActive(true);
        btnOneWrong.gameObject.SetActive(false);
        btnTwoWrongs.gameObject.SetActive(false);
    }

    // Used to play the btn click sound
    public void OnClickPlayBtnSound()
    {
        audioSource.PlayOneShot(sndBtn);
    }

    // Winning coins are just the number of coins that we display in the following scene panel
    private void activeWinningCoins(bool active, int value = 10)
    {
        textOuputCoinsValue.text = "+ " + value.ToString();
        winningCoins.SetActive(active);
        numberOfCoinsWon = value;
        randomMultiplier = Random.Range(2, 5);
        textMultiplier.text = randomMultiplier.ToString();
    }

    // Go back to level map
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // On click start the next level
    public void StartNextSceneLevel()
    {
        if (GameDataV2.Hearts > 0)
        {
            gameManager.StartLevel(GameDataV2.CurrentLevelData.Index + 1, GameDataV2.CurrentLevelData.NextLevelTheme);
        } else
        {
            resultPanel.SetActive(false);
            uiScript.PanelNoHeart.SetActive(true);
            StartCoroutine(forceBackToMenu());
        }
    }

    // Wait 1 sec and then force back to menu
    IEnumerator forceBackToMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LevelMap");
    }

    public void OnClickWatchAd()
    {
        GameDataV2.ServiceCallingWatchAd = "QuizManager";
        watchAdGameObject.SetActive(false);
        watchAd.ShowRewardedVideo("coinsMultiplierRewardedVideo");
    }

    // After the ad the player wins more coins
    public void OnAdFinished()
    {
        GameDataV2.Coins += numberOfCoinsWon * (randomMultiplier - 1);
        textOuputCoinsValue.text = (numberOfCoinsWon * randomMultiplier).ToString();
    }
}
