using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component handling pausing.
/// </summary>
public class GameController : MonoBehaviour
{
    public SongController songController;
    public ActivatorController activatorController;
    public GameObject pausePanel;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    /// <summary>
    /// Pauses the game and activates the pause panel.
    /// </summary>
    private void Pause()
    {
        songController.PauseSong(true);
        Time.timeScale = 0.0f;
        activatorController.PauseActivator(true);
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Unpauses the game and deactivates the pause panel.
    /// </summary>
    public void UnPause()
    {
        songController.PauseSong(false);
        Time.timeScale = 1.0f;
        activatorController.PauseActivator(false);
    }
}
