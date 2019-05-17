using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SongDelayer : MonoBehaviour
{
    public AudioSource song;

    public float delay;

    private void Awake()
    {
        song = GetComponent<AudioSource>();
        song.PlayDelayed(delay);
    }
}
