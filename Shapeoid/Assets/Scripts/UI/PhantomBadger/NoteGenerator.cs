using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public float arrowSpeed = 0.0f;

    private bool isInit = false;
    private SongParser.Metadata songData;
    private float songTimer = 0.0f;
    private float barTime = 0.0f;
    private float barExecutedTime = 0.0f;
    private AudioSource audioSource;
    private SongParser.NoteData noteData;
    private float distance;
    private float originalDistance = 1.0f;
    private int barCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // if done initialising the rest of the world
        // and haven't gone through all bars of the song yet
        if (isInit && barCount < noteData.bars.Count)
        {
            // calculate time offset using s = d /t equation
            // (t = d / s)
            distance = originalDistance;
            float _timeOffset = distance / arrowSpeed;

            // get current time through song
            songTimer = audioSource.time;

            // if current song time - time offset is greater than
            // time taken for all executed bars so far
            // spawn the next bar's notes
            if (songTimer - _timeOffset >= (barExecutedTime - barTime))
            {
                StartCoroutine(PlaceBar(noteData.bars[barCount++]));

                barExecutedTime += barTime;
            }
        }
    }

    private IEnumerator PlaceBar(List<SongParser.Notes> bar)
    {
        for (int i = 0; i < bar.Count; i++)
        {
            if (bar[i].bottom >= 0)
            {
                // instantiate it
            }

            yield return new WaitForSeconds((barTime / bar.Count) - Time.deltaTime);
        }
    }

    public void InitNotes(SongParser.Metadata newSongData)
    {
        songData = newSongData;
        isInit = true;

        // estimate how many seconds a single 'bar' will be in the
        // song using the bpm in song data
        barTime = (60.0f / songData.bpm) * 4.0f;

        distance = originalDistance;

        // how fast the arrow will be going
        arrowSpeed = 0.009f; // TEMPORARY MAGIC NUMBER
        noteData = songData.noteData;
    }
}
