using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int index; //level number
    public string nextLevelTheme; //used to launch next level
    public string type; //used to launch the corresponding scene it can be : Quiz, InputQuiz, TicTacToe or Taquin
    public string theme; //used for Quiz and InputQuiz to know the theme of the question
    public string rightAnswer; //first answer accepted
    public Sprite spriteWithQuestion; //sprite used to illustrate the question
    public float imageScale = 9.182155f; //used to reduce or enlarge the previous sprite
    public string clue; //clue is used to help the user if he buy the corresponding trump card.
    public string info; //used to show informations about the question at the end of the Quiz

    // [Header("Quiz data")]
    public string question; //text to display for the question
    public int fontSizeBtnAnswers; //to reduce or enlarge the font size of answers buttons
    public string answer1; //first proposal if the selected mode by the player is "4 proposals"
    public string answer2; //second proposal if the selected mode by the player is "4 proposals"
    public string answer3; //third proposal if the selected mode by the player is "4 proposals"
    public string answer4; //fourth proposal if the selected mode by the player is "4 proposals"

    // [Header("Logic data")]
    public string difficulty; //used only for the TicTacToe difficulty for now

    // [Header("Input data")]
    public bool needText; //if we need to display a text like for riddles
    public bool needImage; //if we need to display an imagine life for rebus
    public bool needBtnListen; //if we need to play a song
    public AudioClip sound; //the associated sound if the previous property is true
    public string textLevel; //the associated text if "NeedText" is true
    public string otherAcceptedAnswer1; //second answer accepted
    public string otherAcceptedAnswer2; //third answer accepted
    public string otherAcceptedAnswer3; //fourth answer accepted

    public Level()
    {
    }

    public int Index { get => index; set => index = value; }
    public string NextLevelTheme { get => nextLevelTheme; set => nextLevelTheme = value; }
    public string Type { get => type; set => type = value; }
    public string Theme { get => theme; set => theme = value; }
    public string RightAnswer { get => rightAnswer; set => rightAnswer = value; }
    public Sprite SpriteWithQuestion { get => spriteWithQuestion; set => spriteWithQuestion = value; }
    public float ImageScale { get => imageScale; set => imageScale = value; }
    public string Clue { get => clue; set => clue = value; }
    public string Info { get => info; set => info = value; }
    public string Question { get => question; set => question = value; }
    public int FontSizeBtnAnswers { get => fontSizeBtnAnswers; set => fontSizeBtnAnswers = value; }
    public string Answer1 { get => answer1; set => answer1 = value; }
    public string Answer2 { get => answer2; set => answer2 = value; }
    public string Answer3 { get => answer3; set => answer3 = value; }
    public string Answer4 { get => answer4; set => answer4 = value; }
    public string Difficulty { get => difficulty; set => difficulty = value; }
    public bool NeedText { get => needText; set => needText = value; }
    public bool NeedImage { get => needImage; set => needImage = value; }
    public bool NeedBtnListen { get => needBtnListen; set => needBtnListen = value; }
    public AudioClip Sound { get => sound; set => sound = value; }
    public string TextLevel { get => textLevel; set => textLevel = value; }
    public string OtherAcceptedAnswer1 { get => otherAcceptedAnswer1; set => otherAcceptedAnswer1 = value; }
    public string OtherAcceptedAnswer2 { get => otherAcceptedAnswer2; set => otherAcceptedAnswer2 = value; }
    public string OtherAcceptedAnswer3 { get => otherAcceptedAnswer3; set => otherAcceptedAnswer3 = value; }
}
