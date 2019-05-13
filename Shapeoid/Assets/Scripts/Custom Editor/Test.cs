using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component for testing.
/// </summary>
public class Test : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SongParser.Metadata _tempSongData = new SongParser.Metadata();

            _tempSongData.title = "JOURNEI";
            _tempSongData.subtitle = "";
            _tempSongData.artist = "Hans";
            _tempSongData.bannerPath = "";
            _tempSongData.backgroundPath = "";
            _tempSongData.musicPath = "JOURNEI.mp3";
            _tempSongData.offset = 0.0f;
            _tempSongData.sampleStart = 97.33f;
            _tempSongData.sampleLength = 10.58f;
            _tempSongData.bpm = 125;

            SongParser.NoteData _tempNoteData = new SongParser.NoteData();
            List<SongParser.Notes> _tempBar = new List<SongParser.Notes>();
            SongParser.Notes _tempNotes = new SongParser.Notes();

            _tempNotes.bottom = 0;
            _tempNotes.middle = SongParser.NoteType.Air;
            _tempNotes.top = 0;

            _tempBar.Add(_tempNotes);

            _tempNoteData.bars = new List<List<SongParser.Notes>>();
            _tempNoteData.bars.Add(_tempBar);

            _tempSongData.noteData = _tempNoteData;

            SongWriter _songWriter = new SongWriter();
            _songWriter.Write(_tempSongData, Application.persistentDataPath);

            Debug.Log("Song should be written now.");
        }
    }
}
