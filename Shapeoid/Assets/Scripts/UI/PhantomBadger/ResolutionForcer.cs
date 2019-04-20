using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionForcer : MonoBehaviour
{
    public int width = 1366;
    public int height = 768;

    // Update is called once per frame
    private void Update()
    {
        Screen.SetResolution(width, height, false);
    }
}
