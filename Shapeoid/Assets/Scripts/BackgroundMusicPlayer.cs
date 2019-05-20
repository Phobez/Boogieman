using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private static BackgroundMusicPlayer instance = null;

    private AudioSource audioSource;
    private bool isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isPaused = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if ((SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 1) && !isPaused)
        {
            audioSource.Pause();
            isPaused = true;
        }
        else if ((SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 1) && isPaused)
        {
            audioSource.Play();
            isPaused = false;
        }
    }
}
