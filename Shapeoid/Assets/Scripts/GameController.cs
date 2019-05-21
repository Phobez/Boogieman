using TMPro;
using UnityEngine;

/// <summary>
/// A component handling pausing and losing.
/// </summary>
public class GameController : MonoBehaviour
{
    public SongController songController;
    public ActivatorController activatorController;
    public NoteGenerator noteGenerator;
    public ScoreHandler scoreHandler;
    public GameObject pausePanel;
    public GameObject losePanel;
    public TMP_Text songTitle;
    public TMP_Text artist;


    private void Start()
    {
        Time.timeScale = 1.0f;

        songTitle.text = GameData.chosenSongData.title;
        artist.text = GameData.chosenSongData.artist;
    }

    // Update is called once per frame
    private void Update()
    {
        if (scoreHandler.energy <= 0)
        {
            Lose();
        }

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
        noteGenerator.PauseNotes(true);
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
        noteGenerator.PauseNotes(false);
    }

    /// <summary>
    /// Ends the game after energy hits 0.
    /// </summary>
    public void Lose()
    {
        songController.PauseSong(true);
        Time.timeScale = 0.0f;
        activatorController.PauseActivator(true);
        noteGenerator.PauseNotes(true);
        losePanel.SetActive(true);
    }
}
