using UnityEngine;
using UnityEngine.SceneManagement;

// Made by: Abia P.H.
// Description: Holds methods for loading scenes to be used by buttons.
public class LoadSceneOnClick : MonoBehaviour
{
    // loads a scene using its index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
