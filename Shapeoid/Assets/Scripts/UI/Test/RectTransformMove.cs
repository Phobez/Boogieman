using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTransformMove : MonoBehaviour
{
    public RectTransform a;
    public ScrollRect b;
    public RectTransform c;
    public RectTransform d;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            a.anchoredPosition = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            b.verticalNormalizedPosition = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            b.content.anchoredPosition = tempMethod();
        }
    }

    public Vector2 tempMethod()
    {
        Canvas.ForceUpdateCanvases();

        Vector2 viewportLocalPosition = d.localPosition;
        Vector2 childLocalPosition = gameObject.GetComponent<RectTransform>().localPosition;
        Vector2 result = new Vector2(/*0 - (viewportLocalPosition.x + childLocalPosition.x)*/ b.content.localPosition.x, 0 - (viewportLocalPosition.y + childLocalPosition.y));
        return result;
    }
}
