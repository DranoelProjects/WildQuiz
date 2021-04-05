using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenJokersPanelButton : MonoBehaviour
{
    [SerializeField] GameObject jokersPanel;
    Image imageShowJokersPanel;
    [SerializeField] Sprite spriteClose, spriteOpen;

    private void Start()
    {
        //jokersPanel = GameObject.Find("PanelBuyJokers").gameObject;
        imageShowJokersPanel = gameObject.GetComponent<Image>();
        jokersPanel.SetActive(false);
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
            imageShowJokersPanel.color = new Color(255 / 255f, 255 / 255f, 52 / 255f);
        }
    }
}
