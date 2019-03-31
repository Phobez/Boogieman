using UnityEngine;
using UnityEngine.EventSystems;

// Made by: Abia P.H.
// Description: handles scroll content behaviour
public class ScrollContent : MonoBehaviour, ISelectHandler
{
    private ScrollRectController scrollRectController;
    private RectTransform rectTransform;

    private void Awake()
    {
        scrollRectController = GetComponentInParent<ScrollRectController>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name);
        scrollRectController.SnapToSelected(rectTransform);
    }
}
