using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public float score = 0.0f;
    public float multiplier = 1.0f;
    public Text scoreText;

    private SongController songController;
    private AudioSource audioSource;
    private GameObject player;

    private const float scoreVal = 100.0f;

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
    }

    private void AddScore()
    {
        score += scoreVal * multiplier;
    }
}
