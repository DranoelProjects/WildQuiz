using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] string themeLabel;
    [SerializeField] GameObject themeOutput;
    [SerializeField] GameObject btnLabel;
    [SerializeField] GameObject playIcon;
    private int clickCounter = 0;

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

    public void OnClickLevelButton()
    {
        clickCounter++;
        if(clickCounter == 1)
        {
            themeOutput.SetActive(true);
            themeOutput.GetComponent<Text>().text = themeLabel;
            if (!(int.Parse(gameObject.name) == GameDataV2.NextLevel))
            {
                gameObject.GetComponent<Image>().color = Color.blue;
            }
            playIcon.SetActive(true);
            btnLabel.SetActive(false);
            Animator nextLevelAnimator = gameObject.GetComponent<Animator>();
            nextLevelAnimator.SetBool("isNextLevel", true);
        } else if (clickCounter == 2)
        {
            themeOutput.SetActive(false);
            gameObject.GetComponentInParent<LevelMapUI>().OnClickLevelButton(int.Parse(gameObject.name), themeLabel);
            clickCounter = 0;
        }
        StartCoroutine(restButton());
    }

    IEnumerator restButton()
    {
        yield return new WaitForSeconds(6f);
        playIcon.SetActive(false);
        btnLabel.SetActive(true);
        themeOutput.SetActive(false);
        clickCounter = 0;

        Animator nextLevelAnimator = gameObject.GetComponent<Animator>();
        if(int.Parse(gameObject.name) == GameDataV2.NextLevel)
        {
            //init next level color
            gameObject.GetComponent<Image>().color = Color.green;
        } else
        {
            nextLevelAnimator.SetBool("isNextLevel", false);
            gameObject.GetComponent<Image>().color = new Color(113f / 255f, 105f / 255f, 105f / 255f);
        }
    }
}
