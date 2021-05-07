using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameLevel/New level")]
public class LevelData : ScriptableObject
{
    [Header("Common data")]
    public int Level; //level number
    public string Type = "Quiz"; //used to launch the corresponding scene it can be : Quiz, InputQuiz, TicTacToe or Taquin
    public string Theme; //used for Quiz and InputQuiz to know the theme of the question
    public string RightAnswer; //first answer accepted
    public Sprite SpriteWithQuestion; //sprite used to illustrate the question
    public float ImageScale = 7.51158f; //used to reduce or enlarge the previous sprite
    public string Clue; //clue is used to help the user if he buy the corresponding trump card.
    public string Info; //used to show informations about the question at the end of the Quiz

    [Header("Next Level Data")]
    public LevelData NextLevelData; //used to store the next level data in order to start the next level

    [Header("Quiz data")]
    public string Question; //text to display for the question
    public int FontSizeBtnAnswers = 150; //to reduce or enlarge the font size of answers buttons
    public string Answer1; //first proposal if the selected mode by the player is "4 proposals"
    public string Answer2; //second proposal if the selected mode by the player is "4 proposals"
    public string Answer3; //third proposal if the selected mode by the player is "4 proposals"
    public string Answer4; //fourth proposal if the selected mode by the player is "4 proposals"

    [Header("Logic data")]
    public string Difficulty; //used only for the TicTacToe difficulty for now

    [Header("Input data")]
    public bool NeedText = false; //if we need to display a text like for riddles
    public bool NeedImage = false; //if we need to display an imagine life for rebus
    public bool NeedBtnListen = false; //if we need to play a song
    public AudioClip Sound; //the associated sound if the previous property is true
    public string TextLevel; //the associated text if "NeedText" is true
    public string OtherAcceptedAnswer1; //second answer accepted
    public string OtherAcceptedAnswer2; //third answer accepted
    public string OtherAcceptedAnswer3; //fourth answer accepted
}
