using Firebase.Database;
using UnityEngine;
using Firebase.Extensions; // for ContinueWithOnMainThread

public class ApiManager : MonoBehaviour
{
    private FirebaseDatabase _database;
    private const string DATABASE_URL = "https://wildquiz-41011941-default-rtdb.europe-west1.firebasedatabase.app/";
    // [SerializeField] LevelData[] levelDatas;

    void Awake()
    {
        _database = FirebaseDatabase.GetInstance(DATABASE_URL);
        /* foreach (LevelData levelData in levelDatas)
        {
            setLevel(levelData);
        }*/
    }

    /*void setLevel(LevelData levelData)
    {
        Level level = new Level();
        level.Index = levelData.Level;
        level.Type = levelData.Type;
        level.Theme = levelData.Theme;
        level.RightAnswer = levelData.RightAnswer;
        if (levelData.SpriteWithQuestion)
            level.SpriteWithQuestionName = levelData.SpriteWithQuestion.name;
        level.ImageScale = levelData.ImageScale;
        level.Clue = levelData.Clue;
        level.Info = levelData.Info;
        level.Question = levelData.Question;
        level.FontSizeBtnAnswers = levelData.FontSizeBtnAnswers;
        level.Answer1 = levelData.Answer1;
        level.Answer2 = levelData.Answer2;
        level.Answer3 = levelData.Answer3;
        level.Answer4 = levelData.Answer4;
        level.Difficulty = levelData.Difficulty;
        level.NeedText = levelData.NeedText;
        level.NeedImage = levelData.NeedImage;
        level.NeedBtnListen = levelData.NeedBtnListen;
        if(levelData.Sound)
            level.SoundName = levelData.Sound.name;
        level.TextLevel = levelData.TextLevel;
        level.OtherAcceptedAnswer1 = levelData.OtherAcceptedAnswer1;
        level.OtherAcceptedAnswer2 = levelData.OtherAcceptedAnswer2;
        level.OtherAcceptedAnswer3 = levelData.OtherAcceptedAnswer3;

        string json = JsonUtility.ToJson(level);
        _database.GetReference("pages").Child("5").Child("levels").Child(levelData.Level.ToString()).SetRawJsonValueAsync(json);
    }*/

    void getString()
    {
        _database.GetReference("pages").Child("levels")
          .GetValueAsync().ContinueWithOnMainThread(task => {
              if (task.IsFaulted)
              {
                  Debug.Log("error0");
              }
              else if (task.IsCompleted)
              {
                  DataSnapshot snapshot = task.Result;
                  string json = JsonUtility.ToJson(snapshot);
                  Debug.Log(snapshot);
                  _database.GetReference("pages").Child("1").Child("levels").SetRawJsonValueAsync(json);
              }
          });
    }
    
}
