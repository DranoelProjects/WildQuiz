using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = gameObject.GetComponent<ScrollRect>();
    }

    // Used to follow with the camera
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

