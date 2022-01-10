using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private void Start()
    {
        // Init color of the button
        if (!PlayerPrefs.HasKey("NextLevel"))
        {
            GameDataV2.NextLevel = 1;
        }
        if (GameDataV2.NextLevel < int.Parse(gameObject.name))
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Image>().color = Color.black;
        }
    }
}
