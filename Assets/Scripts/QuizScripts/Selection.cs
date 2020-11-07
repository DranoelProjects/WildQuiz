using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour
{
    QuizManager quizManager;


    void Start()
    {
        quizManager = GameObject.Find("QuizManager").GetComponent<QuizManager>();
    }

    public void CheckAnswer()
    {
        string playerAnswer = GetComponentInChildren<Text>().text;
        quizManager.CheckAnswer(playerAnswer);
    }

}
