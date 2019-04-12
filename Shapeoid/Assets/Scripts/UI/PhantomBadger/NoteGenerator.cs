using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public GameObject fireNote;
    public GameObject airNote;
    public GameObject waterNote;
    public GameObject earthNote;

    public GameObject bottomLane;
    public GameObject middleLane;
    public GameObject topLane;

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
        audioSource = GetComponent<AudioSource>();
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

    // go through all notes in a bar
    // creates an instance of note prefab
    // depending on which note is meant to be spawned
    private IEnumerator PlaceBar(List<SongParser.Notes> bar)
    {
        for (int i = 0; i < bar.Count; i++)
        {
            if (bar[i].bottom >= 0)
            {
                GameObject _obj = (GameObject) Instantiate(GetNotePrefab(bar[i].bottom), new Vector3(bottomLane.transform.position.x + distance, bottomLane.transform.position.y, bottomLane.transform.position.z - 0.3f), Quaternion.identity);
            }
            if (bar[i].middle >= 0)
            {
                GameObject _obj = (GameObject) Instantiate(GetNotePrefab(bar[i].middle), new Vector3(middleLane.transform.position.x + distance, middleLane.transform.position.y, middleLane.transform.position.z - 0.3f), Quaternion.identity);
            }
            if (bar[i].top >= 0)
            {
                GameObject _obj = (GameObject) Instantiate(GetNotePrefab(bar[i].top), new Vector3(topLane.transform.position.x + distance, topLane.transform.position.y, topLane.transform.position.z - 0.3f), Quaternion.identity);
            }

            yield return new WaitForSeconds((barTime / bar.Count) - Time.deltaTime);
        }
    }

    // iniatialises variables and sets arrow speed
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

    // returns appropriate note prefab
    private GameObject GetNotePrefab(SongParser.NoteType noteType)
    {
        GameObject _obj = null;

        switch (noteType)
        {
            case SongParser.NoteType.Fire:
                _obj = fireNote;
                break;
            case SongParser.NoteType.Air:
                _obj = airNote;
                break;
            case SongParser.NoteType.Water:
                _obj = waterNote;
                break;
            case SongParser.NoteType.Earth:
                _obj = earthNote;
                break;
            default:
                _obj = fireNote;
                break;
        }

        return _obj;
    }
}
