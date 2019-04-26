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

    private SongController songController;
    private AudioSource audioSource;
    private GameObject player;
    private int streak = 0;

    private const float scoreVal = 100.0f;
    private const int powerVal = 50;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        songController = player.GetComponent<SongController>();
        audioSource = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = "Score: " + score;
        streakText.text = "Streak: " + streak;
        multiplierText.text = "Multiplier: " + multiplier + "x";
    }

    // adds score by scoreVal times the current value of multiplier
    private void AddScore()
    {
        score += scoreVal * multiplier;
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
