using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;

public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject panelNextScene, jokersPanel, cluePanel, panelAnswersButtons, panelInputAnswer;

    Button[] allButtons;
    Text result;

    [SerializeField] Image imageShowJokersPanel;
    [SerializeField] Sprite spriteClose, spriteOpen;

    [SerializeField] Button btnOneWrong, btnTwoWrongs, btnBuyClue;

    [Header("Load quiz data")]
    public Level levelData;
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

    private void Awake()
    {
        uiScript = GameObject.Find("GameObjectUI").GetComponent<UIScript>();
        result = panelNextScene.GetComponentInChildren<Text>();
        levelData = GameDataV2.CurrentLevelData;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        allButtons = GameObject.Find("PanelAnswersButtons").GetComponentsInChildren<Button>();
        loadLevelData();
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
                RevealAnswer(false);
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
            questionText.enabled = false;
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
    public void RevealAnswer(bool isRightAnswer)
    {
        isTimerActivate = false;
        if (levelData.Type == "Quiz")
        {
            foreach (Button btn in allButtons)
            {
                string btnAnswer = btn.GetComponentInChildren<Text>().text;
                if ((levelData.RightAnswer == btnAnswer) && isRightAnswer)
                {
                    btn.GetComponent<Image>().color = Color.green;
                }
                else if (isRightAnswer)
                {
                    btn.GetComponent<Image>().color = Color.red;
                }
                btn.GetComponent<Button>().interactable = false;
            }
        }
        int nextLevel = levelData.Index + 1;
        if (GameDataV2.NextLevel < nextLevel)
        {
            GameDataV2.NextLevel = nextLevel;
        }
        clue.text = levelData.Info;
        textAnswerWas.text = levelData.RightAnswer;
        textAnswerWas.color = Color.green;
        if (!cluePanel.gameObject.activeInHierarchy)
        {
            ShowCluePanel();
        }
        StartCoroutine(nextScene());
    }

    // Wait 2 sec then display the following scene panel
    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2f);
        panelNextScene.SetActive(true);
        gameObjectAnswerWas.SetActive(true);
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
        panelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(active, value);
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
        CheckAnswer(inputField.text);
    }

    // For input quiz or directly mode, check the answer
    public void CheckAnswer(string playerAnswer)
    {
        bool isRightAnswer = false;
        audioSource.Stop();
        ShowWinningCoins(false);
        double threshold = 0.8;
        if ((Levenshtein.ComputeCorrelation(levelData.RightAnswer, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer1, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer2, playerAnswer, false) > threshold)
            || (Levenshtein.ComputeCorrelation(levelData.OtherAcceptedAnswer3, playerAnswer, false) > threshold))
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
        RevealAnswer(isRightAnswer);
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

}
