using UnityEngine;

public class Beatmap : MonoBehaviour
{
    private AudioSource audioSource;

    private AudioClip song;

    private float songLength;
    private float currentSongTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        song = audioSource.clip;

        songLength = song.length;
        Debug.Log(songLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        currentSongTime = audioSource.time;

        int _index = (int) (currentSongTime * 100);
        Debug.Log(_index);
    }
}

//public class Time
//{
//    public Time(byte noteType)
//    {
//        this.noteType = noteType;
//    }

//    private byte noteType;
//}