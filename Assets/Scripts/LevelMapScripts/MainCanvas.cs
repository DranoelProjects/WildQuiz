using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    RectTransform scrollRectTarget;

    ScrollRect scrollRect;
    Animator nextLevelAnimator;
    Image nextLevelImage;

    private void Awake()
    {
        scrollRect = gameObject.GetComponent<ScrollRect>();
    }

    private void Start()
    {
        initNextLevelColor();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>().PlayMusic();
    }

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        var contentPos = (Vector2)scrollRect.transform.InverseTransformPoint(scrollRect.content.position);
        var childPos = (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        var endPos = contentPos - childPos;
        // If no horizontal scroll, then don't change contentPos.x
        if (!scrollRect.horizontal) endPos.x = contentPos.x;
        // If no vertical scroll, then don't change contentPos.y
        if (!scrollRect.vertical) endPos.y = contentPos.y;
        scrollRect.content.anchoredPosition = endPos;
    }

    void initNextLevelColor()
    {
        MainCanvas mainCanvasScript = gameObject.GetComponent<MainCanvas>();
        //init next level anim
        nextLevelAnimator = GameObject.Find(PlayerPrefs.GetInt("NextLevel").ToString()).GetComponent<Animator>();
        nextLevelAnimator.SetBool("isNextLevel", true);

        //init next level color
        nextLevelImage = GameObject.Find(PlayerPrefs.GetInt("NextLevel").ToString()).GetComponent<Image>();
        nextLevelImage.color = Color.green;

        //init scroll rect target
        scrollRectTarget = GameObject.Find(PlayerPrefs.GetInt("NextLevel").ToString()).GetComponent<RectTransform>();
        mainCanvasScript.SnapTo(scrollRectTarget);
    }
}
