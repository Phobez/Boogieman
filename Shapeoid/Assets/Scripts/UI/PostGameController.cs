using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostGameController : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text notesHitText;
    public TMP_Text longestStreakText;
    public TMP_Text songTitleText;

    // Start is called before the first frame update
    private void Start()
    {
        totalScoreText.text = GameData.currentSongStats.totalScore.ToString();

        float _notesHitPercentage = (float)GameData.currentSongStats.notesHit / (float)GameData.currentSongStats.notesCounter * 100.0f;
        _notesHitPercentage = (float) Math.Round(_notesHitPercentage, 2);

        notesHitText.text = _notesHitPercentage.ToString();

        longestStreakText.text = GameData.currentSongStats.longestStreak.ToString();

        songTitleText.text = GameData.chosenSongData.title;
    }
}
