using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PulseText : MonoBehaviour
{
    // pulse parameters
    public float approachSpeed = 0.02f;
    public float growthBound = 2f;
    public float shrinkBound = 0.5f;
    private float currentRatio = 1;

    // text object
    private TMP_Text text;

    private void Awake()
    {
        // find the text element
        this.text = this.gameObject.GetComponent<TMP_Text>();
    }

    public void Pulse()
    {
        StartCoroutine(CPulse());
    }

    public IEnumerator CPulse()
    {
        // get bigger
        while (this.currentRatio != this.growthBound)
        {
            // determine the new ratio to use
            currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

            // update text element
            this.text.transform.localScale = Vector3.one * currentRatio;

            yield return new WaitForEndOfFrame();
        }

        // shrink
        while (this.currentRatio != this.shrinkBound)
        {
            // determine the new ratio to use
            currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

            // update text element
            this.text.transform.localScale = Vector3.one * currentRatio;

            yield return new WaitForEndOfFrame();
        }
    }
}