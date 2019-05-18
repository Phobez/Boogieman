using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPaused;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
        isPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && !isPaused)
        {
            audioSource.Pause();
            isPaused = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex != 2 && isPaused)
        {
            audioSource.Play();
            isPaused = false;
        }
    }
}
