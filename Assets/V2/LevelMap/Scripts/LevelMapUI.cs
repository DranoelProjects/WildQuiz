using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapUI : MonoBehaviour
{
    private GameManager gameManager;
    Animator nextLevelAnimator;
    Image nextLevelImage;
    RectTransform scrollRectTarget;
    ScrollRect scrollRect;
    private UIScript uiScript;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scrollRect = gameObject.GetComponent<ScrollRect>();
        uiScript = gameObject.GetComponentInChildren<UIScript>();
    }

    void Start()
    {
        initNextLevelColor();
    }

    public void OnClickLevelButton(int selectedLevel, string theme)
    {
        try
        {
            if (GameDataV2.Hearts > 0)
            {
                gameManager.StartLevel(selectedLevel, theme);
            }
            else
            {
                uiScript.PanelNoHeart.SetActive(true);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    void initNextLevelColor()
    {
        //init next level anim
        int nextLevel = GameDataV2.NextLevel;
        if (nextLevel == 86)
        {
            nextLevel--;
            GameObject nextLevelGameObject = GameObject.Find((nextLevel).ToString());
            nextLevelAnimator = nextLevelGameObject.GetComponent<Animator>();
            nextLevelAnimator.SetBool("isNextLevel", false);

            //init next level color
            nextLevelImage = nextLevelGameObject.GetComponent<Image>();
            nextLevelImage.color = Color.yellow;

            //init scroll rect target
            scrollRectTarget = nextLevelGameObject.GetComponent<RectTransform>();
            SnapTo(scrollRectTarget);
        }
        else
        {
            GameObject nextLevelGameObject = GameObject.Find((nextLevel).ToString());
            nextLevelAnimator = nextLevelGameObject.GetComponent<Animator>();
            nextLevelAnimator.SetBool("isNextLevel", true);

            //init next level color
            nextLevelImage = nextLevelGameObject.GetComponent<Image>();
            nextLevelImage.color = Color.green;

            //init scroll rect target
            scrollRectTarget = nextLevelGameObject.GetComponent<RectTransform>();
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
