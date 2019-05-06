using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A component to load a scene on key down.
/// </summary>
public class LoadSceneOnKeyDown : MonoBehaviour
{
    public KeyCode keyToPress;
    public int targetSceneIndex;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            LoadSceneByIndex(targetSceneIndex);
        }
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
