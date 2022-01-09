using System.Collections;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

public static class LevelMapper
{
    public static Level LevelMapperWithFirebaseSnapshot(DataSnapshot snapshot)
    {
        Level level = new Level();
        level.Index = int.Parse(snapshot.Child("index").GetValue(true).ToString());
        level.Type = snapshot.Child("type").GetValue(true).ToString();
        level.Theme = snapshot.Child("theme").GetValue(true).ToString();
        level.RightAnswer = snapshot.Child("rightAnswer").GetValue(true).ToString();
        level.SpriteWithQuestionName = snapshot.Child("spriteWithQuestionName").GetValue(true).ToString();
        level.ImageScale = float.Parse(snapshot.Child("imageScale").GetValue(true).ToString());
        level.Clue = snapshot.Child("clue").GetValue(true).ToString();
        level.Info = snapshot.Child("info").GetValue(true).ToString();
        level.Question = snapshot.Child("question").GetValue(true).ToString();
        level.FontSizeBtnAnswers = int.Parse(snapshot.Child("fontSizeBtnAnswers").GetValue(true).ToString());
        level.Answer1 = snapshot.Child("answer1").GetValue(true).ToString();
        level.Answer2 = snapshot.Child("answer2").GetValue(true).ToString();
        level.Answer3 = snapshot.Child("answer3").GetValue(true).ToString();
        level.Answer4 = snapshot.Child("answer4").GetValue(true).ToString();
        level.Difficulty = snapshot.Child("difficulty").GetValue(true).ToString();
        level.NeedText = snapshot.Child("needText").GetValue(true).ToString() == "true";
        level.NeedImage = snapshot.Child("needImage").GetValue(true).ToString() == "true";
        level.NeedBtnListen = snapshot.Child("needBtnListen").GetValue(true).ToString() == "true";
        level.SoundName = snapshot.Child("soundName").GetValue(true).ToString();
        level.TextLevel = snapshot.Child("textLevel").GetValue(true).ToString();
        level.OtherAcceptedAnswer1 = snapshot.Child("otherAcceptedAnswer1").GetValue(true).ToString();
        level.OtherAcceptedAnswer2 = snapshot.Child("otherAcceptedAnswer2").GetValue(true).ToString();
        level.OtherAcceptedAnswer3 = snapshot.Child("otherAcceptedAnswer3").GetValue(true).ToString();
        return level;
    }
}
