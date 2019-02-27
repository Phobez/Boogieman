using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int noteValue = 100;
    private int multiplier;
    private int streak;

    // Start is called before the first frame update
    private void Start()
    {
        multiplier = 1;
        streak = 0;

        UpdateGUIValues();
    }

    // a method to increase the streak and determine the current multiplier
    public void AddStreak()
    {
        streak++;

        if (streak >= 24) multiplier = 4;
        else if (streak >= 16) multiplier = 3;
        else if (streak >= 8) multiplier = 2;
        else multiplier = 1;

        UpdateGUIValues();
    }

    // a method to reset the streak
    public void ResetStreak()
    {
        streak = 0;
        multiplier = 1;

        UpdateGUIValues();
    }

    // a method to update the player prefs values
    public void UpdateGUIValues()
    {
        PlayerPrefs.SetInt("Multiplier", multiplier);
        PlayerPrefs.SetInt("Streak", streak);
    }

    // a method to return the points of a hit note
    public int GetScore()
    {
        return noteValue * multiplier;
    }
}
