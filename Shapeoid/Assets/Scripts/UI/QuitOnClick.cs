using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Abia P.H.
// Description: Holds a method to quit the game to be used by buttons.
public class QuitOnClick : MonoBehaviour
{
    // exits play mode if in editor, exits the game if not
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            SaveLoad.Save();
            Application.Quit();
        #endif
    }
}
