using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelMapUI : MonoBehaviour
{
    private GameManager gameManager;
    Animator nextLevelAnimator;
    Image nextLevelImage;
    RectTransform scrollRectTarget;
    ScrollRect scrollRect;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scrollRect = gameObject.GetComponent<ScrollRect>();
    }

    void Start()
    {
        initNextLevelColor();
    }

    public void OnClickLevelButton()
    {
        gameManager.StartLevel(int.Parse(EventSystem.current.currentSelectedGameObject.name));
    }

    void initNextLevelColor()
    {
        //init next level anim
        int nextLevel = GameDataV2.NextLevel;
        if (nextLevel == 86)
        {
            nextLevel--;
            nextLevelAnimator = GameObject.Find((nextLevel).ToString()).GetComponent<Animator>();
            nextLevelAnimator.SetBool("isNextLevel", false);

            //init next level color
            nextLevelImage = GameObject.Find(nextLevel.ToString()).GetComponent<Image>();
            nextLevelImage.color = Color.yellow;

            //init scroll rect target
            scrollRectTarget = GameObject.Find(nextLevel.ToString()).GetComponent<RectTransform>();
            SnapTo(scrollRectTarget);
        }
        else
        {
            nextLevelAnimator = GameObject.Find(nextLevel.ToString()).GetComponent<Animator>();
            nextLevelAnimator.SetBool("isNextLevel", true);

            //init next level color
            nextLevelImage = GameObject.Find(nextLevel.ToString()).GetComponent<Image>();
            nextLevelImage.color = Color.green;

            //init scroll rect target
            scrollRectTarget = GameObject.Find(nextLevel.ToString()).GetComponent<RectTransform>();
            SnapTo(scrollRectTarget);
        }
    }

    // Used to focus with te camera
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

}
