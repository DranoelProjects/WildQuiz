﻿using System.Collections;
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
    public LevelData levelData;
    [SerializeField] Text questionText, answer1, answer2, answer3, answer4, clue, theme;
    [SerializeField] Image imageWithQuestion;
    GameManagerScript gameManagerScript;

    [Header("Timer")]
    float timeLeft = 20.0f;
    bool isTimerActivate = true;
    [SerializeField] Text textTimer;
    AudioSource audioSource;
    [SerializeField] AudioClip sndWrong, sndWin, sndSardoche;

    [Header("InputQuiz")]
    [SerializeField] ListenSound listenSound;
    [SerializeField] GameObject textLevel, btnListen, gameObjectAnswerWas;
    [SerializeField] InputField answerInput;
    [SerializeField] Text textAnswerWas;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        result = panelNextScene.GetComponentInChildren<Text>();
        levelData = gameManagerScript.LevelData;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().StopMusic();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        allButtons = GameObject.Find("PanelAnswersButtons").GetComponentsInChildren<Button>();
        gameManagerScript.Mute(PlayerPrefs.GetInt("mute") == 1);
        loadLevelData();
    }

    void Update()
    {
        if (isTimerActivate)
        {
            timeLeft -= Time.deltaTime;
            textTimer.text = (timeLeft).ToString("0");
            if (timeLeft < 0)
            {
                isTimerActivate = false;
                audioSource.Stop();
                ShowWinningCoins(false);
                if (levelData.Level == PlayerPrefs.GetInt("NextLevel"))
                {
                    PlayerPrefs.SetInt("NumberLostLevels", PlayerPrefs.GetInt("NumberLostLevels") + 1);
                }
                audioSource.PlayOneShot(sndWrong);
                PlayerPrefs.SetInt("HeartsNumber", PlayerPrefs.GetInt("HeartsNumber") - 1);
                GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateHearts();
                WriteResult("Temps écoulé !");
                RevealAnswer();
            }
        }
        else
        {
            timeLeft = 0.0f;
            textTimer.text = (timeLeft).ToString("0");
        }
    }

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
        } else if (levelData.Type == "InputQuiz")
        {
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

    public void RevealAnswer()
    {
        isTimerActivate = false;
        if(levelData.Type == "Quiz")
        {
            foreach (Button btn in allButtons)
            {
                string btnAnswer = btn.GetComponentInChildren<Text>().text;
                if (levelData.RightAnswer == btnAnswer)
                {
                    btn.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    btn.GetComponent<Image>().color = Color.red;
                }
                btn.GetComponent<Button>().interactable = false;
            }
        }
        int nextLevel = levelData.Level + 1;
        if (PlayerPrefs.GetInt("NextLevel") < nextLevel)
        {
            PlayerPrefs.SetInt("NextLevel", nextLevel);
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

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2f);
        panelNextScene.SetActive(true);
        gameObjectAnswerWas.SetActive(true);
    }

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

    public void ShowWinningCoins(bool active)
    {
        panelNextScene.GetComponent<NextSceneScript>().ActiveWinningCoins(active);
    }

    public void WriteResult(string text)
    {
        result.text = text;
    }


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
            imageShowJokersPanel.color = Color.white;
        }
    }

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

    public void OnClickCheckInput() {
        InputField inputField = answerInput.GetComponent<InputField>();
        CheckAnswer(inputField.text);
    }

    public void CheckAnswer(string playerAnswer)
    {
        audioSource.Stop();
        ShowWinningCoins(false);
        playerAnswer = playerAnswer.ToLower();
        if (levelData.RightAnswer.ToLower() == playerAnswer 
            || (levelData.OtherAcceptedAnswer1 != "" && levelData.OtherAcceptedAnswer1 == playerAnswer)
            || (levelData.OtherAcceptedAnswer2 != "" && levelData.OtherAcceptedAnswer2 == playerAnswer) 
            || (levelData.OtherAcceptedAnswer3 != "" && levelData.OtherAcceptedAnswer3 == playerAnswer))
        {
            audioSource.PlayOneShot(sndWin);
            if (levelData.Level == PlayerPrefs.GetInt("NextLevel"))
            {
                PlayerPrefs.SetInt("NumberWonLevels", PlayerPrefs.GetInt("NumberWonLevels") + 1);
                ShowWinningCoins(true);
                PlayerPrefs.SetInt("CoinsNumber", PlayerPrefs.GetInt("CoinsNumber") + 10);
                GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateCoins();
            }
            WriteResult("Bonne réponse !");
        }
        else
        {
            if (levelData.Level == PlayerPrefs.GetInt("NextLevel"))
            {
                PlayerPrefs.SetInt("NumberLostLevels", PlayerPrefs.GetInt("NumberLostLevels") + 1);
            }
            int random = Random.Range(1, 6);
            if (random == 1)
            {
                audioSource.PlayOneShot(sndSardoche);
            }
            else
            {
                audioSource.PlayOneShot(sndWrong);
            }
            PlayerPrefs.SetInt("HeartsNumber", PlayerPrefs.GetInt("HeartsNumber") - 1);
            GameObject.Find("PanelUserInfo").GetComponent<PanelUserInfo>().UpdateHearts();
            WriteResult("Mauvaise réponse !");
        }
        RevealAnswer();

    }
}
