using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{
    [SerializeField] Text currentLevelNumber, lostLevelsNumber, wonLevelsNumber;
    [SerializeField] GameObject gameObjectStats, textCredits, helpPanel;
    [SerializeField] Button btnShowStats, btnShowCredits;
    bool isShowingStats = false, isShowingsCredits = false;

    AudioSource audioSource;
    [SerializeField] AudioClip sndClick;

    public void OnClickShowStats()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        if (!isShowingStats)
        {
            btnShowStats.GetComponent<Image>().color = Color.green;
            gameObjectStats.SetActive(true);
            currentLevelNumber.text = PlayerPrefs.GetInt("NextLevel").ToString();
            lostLevelsNumber.text = PlayerPrefs.GetInt("NumberLostLevels").ToString();
            wonLevelsNumber.text = PlayerPrefs.GetInt("NumberWonLevels").ToString();
            isShowingsCredits = true;
            OnClickShowCredits();
        } else
        {
            btnShowStats.GetComponent<Image>().color = new Color(255/255f, 255/255f, 255/255f);
            gameObjectStats.SetActive(false);
        }
        isShowingStats = !isShowingStats;
    }

    public void OnClickShowCredits()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        if (!isShowingsCredits)
        {
            btnShowCredits.GetComponent<Image>().color = Color.green;
            textCredits.SetActive(true);
            isShowingStats = true;
            OnClickShowStats();
        }
        else
        {
            btnShowCredits.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            textCredits.SetActive(false);
        }
        isShowingsCredits = !isShowingsCredits;
    }

    public void OnClickOpenDiscord()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        Application.OpenURL("https://discord.gg/mB3dBWfg8K");
    }

    public void OnClickShowHelpPanel()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        audioSource.PlayOneShot(sndClick);
        bool isHelpPanelActive = !helpPanel.gameObject.activeInHierarchy;
        helpPanel.gameObject.SetActive(isHelpPanelActive);
    }
}
