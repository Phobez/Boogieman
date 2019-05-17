using UnityEngine;
using UnityEngine.UI;

// Made by: Abia P.H.
// Description: Contains the method SnapToSelected (see description below)
public class ScrollRectController : MonoBehaviour
{
    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Description: snaps the Scroll Rect Game Object to the currently selected child
    public void SnapToSelected(RectTransform child)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 _viewportAnchoredPosition = scrollRect.viewport.anchoredPosition;
        Vector2 _childAnchoredPosition = child.anchoredPosition;
        Vector2 _result = new Vector2(_viewportAnchoredPosition.x, 0 - (_viewportAnchoredPosition.y + _childAnchoredPosition.y));

        scrollRect.content.anchoredPosition = _result;
    }
}
