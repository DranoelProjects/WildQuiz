using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameLevel/New level")]
public class LevelData : ScriptableObject
{
    [Header("Common data")]
    public int Level;
    public string Type = "Quiz";
    public string Theme;
    public string RightAnswer;
    public Sprite SpriteWithQuestion;
    public float ImageScale = 7.51158f;
    public string Clue;
    public string Info;

    [Header("Next Level Data")]
    public LevelData NextLevelData;

    [Header("Quiz data")]
    public string Question;
    public int FontSizeBtnAnswers = 150;
    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string Answer4;

    [Header("Logic data")]
    public string Difficulty;

    [Header("Input data")]
    public bool NeedText = false;
    public bool NeedImage = false;
    public bool NeedBtnListen = false;
    public AudioClip Sound;
    public string TextLevel;
    public string OtherAcceptedAnswer1;
    public string OtherAcceptedAnswer2;
    public string OtherAcceptedAnswer3;
}
