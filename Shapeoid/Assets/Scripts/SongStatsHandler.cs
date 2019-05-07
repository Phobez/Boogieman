using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongStatsHandler
{
    public struct SongStats
    {
        public float totalScore;
        public int notesHit;
        public int notesCounter;
        public int longestStreak;
    }

    public void ClearData(SongStats songStats)
    {
        songStats.totalScore = 0.0f;
        songStats.notesHit = 0;
        songStats.notesCounter = 0;
        songStats.longestStreak = 0;
    }
}
