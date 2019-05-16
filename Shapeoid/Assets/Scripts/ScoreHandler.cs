using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles score, power, and energy.
/// </summary>
public class ScoreHandler : MonoBehaviour
{
    public float score = 0.0f;
    public int multiplier = 1;
    public int power = 0;
    public int energy = 25;
    public Text scoreText;
    public Text streakText;
    public Text multiplierText;
    public Text powerText;
    public Text energyText;


    private SongController songController;
    private AudioSource audioSource;
    private NoteGenerator noteGenerator;
    private GameObject player;
    private int streak = 0;
    //private float perfectOffset;
    //private float greatOffset;
    //private float accuracyMultiplier;
    //private float hitOffset;

    private SongStatsHandler songStatsHandler;

    private const float scoreVal = 100.0f;
    private const int powerVal = 50;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        songController = player.GetComponent<SongController>();
        audioSource = player.GetComponent<AudioSource>();
        noteGenerator = GetComponent<NoteGenerator>();

        SongStatsHandler songStatsHandler = new SongStatsHandler();
        songStatsHandler.ClearData(GameData.currentSongStats);

        hitOffset = noteGenerator.hitOffset;
        perfectOffset = hitOffset - 0.1f;
        greatOffset = hitOffset - 0.25f;
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "Score: " + score;
        streakText.text = "Streak: " + streak;
        multiplierText.text = "Multiplier: " + multiplier + "x";
        powerText.text = "Power: " + power;
        energyText.text = "Energy: " + energy;
    }

    // adds score by scoreVal times the current value of multiplier
    private void AddScore(float accuracyMultiplier)
    {
        //if(notePos >= player.transform.position.x - hitOffset + perfectOffset && notePos <= player.transform.position.x + hitOffset - perfectOffset)
        //{
        //    accuracyMultiplier = 1.2f;   
        //}
        //else if (notePos >= player.transform.position.x - hitOffset + greatOffset && notePos <= player.transform.position.x + hitOffset - greatOffset)
        //{
        //    accuracyMultiplier = 1.1f;
        //}
        //else if (notePos >= player.transform.position.x - hitOffset && notePos <= player.transform.position.x + hitOffset)
        //{
        //    accuracyMultiplier = 1f;
        //}
        score += scoreVal * multiplier * accuracyMultiplier;
        Debug.Log(accuracyMultiplier);
        GameData.currentSongStats.totalScore = score;
        AddEnergy();
        AddStreak();
    }

    // adds streak and determines current multiplier value
    private void AddStreak()
    {
        streak++;

        if (multiplier <= 4)
        {
            if (streak >= 24)
            {
                multiplier = 4;
            }
            else if (streak >= 16)
            {
                multiplier = 3;
            }
            else if (streak >= 8)
            {
                multiplier = 2;
            }
            else
            {
                multiplier = 1;
            }
        }
        else if (streak < 8)
        {
            multiplier = 1;
        }

        if (streak > GameData.currentSongStats.longestStreak)
        {
            GameData.currentSongStats.longestStreak = streak;
        }
    }

    private void ResetStreak()
    {
        streak = 0;
        multiplier = 1;
    }

    // adds power by powerVal, validates it does not go over 100
    private void AddPower()
    {
        power = Mathf.Clamp(power + powerVal, 0, 100);
        AddStreak();
    }

    // adds energy by 1, validates it does not go over 50
    private void AddEnergy()
    {
        if (energy + 1 <= 50)
        {
            energy++;
        }
    }

    private void LoseEnergy()
    {
        energy -= 2;
    }
}
