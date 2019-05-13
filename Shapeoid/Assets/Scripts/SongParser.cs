using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Made by      : Abia P.H.
// Description  : Contains structs that contain metadata for a track
public class SongParser
{
    // enum of note types
    // long notes have two components: a start and an ending
    // the start determines where the long note starts
    // the long note doesn't end until it encounters an end
    public enum NoteType : byte
    {
        // Idle,
        Fire = 1,
        Air,
        Water,
        Earth
    };

    private string filePath;

    private float sampleLengthDefault = 15.0f;

    // contains all information for a track
    public struct Metadata
    {
        // is the track structure valid
        public bool valid;

        // song title, subtitle, artist
        public string title;
        public string subtitle;
        public string artist;

        // file paths for related images and song media
        public string bannerPath;
        public string backgroundPath;
        public string musicPath;

        // offset that the song starts at compared to the note info
        public float offset;

        // start and length of the sample that is played when selected
        public float sampleStart;
        public float sampleLength;

        // beats per minute
        public float bpm;

        // note data as well as a boolean to check that data exists
        public NoteData noteData;
        public bool noteDataExists;
    }

    // contains all the bars for a song
    public struct NoteData
    {
        public List<List<Notes>> bars;
    }

    // contains note information for a single 'row' of notes
    public struct Notes
    {
        public NoteType top;
        public NoteType middle;
        public NoteType bottom;
    }

    // parsing the note file
    public Metadata Parse(string newFilePath)
    {
        filePath = newFilePath;

        // check if the file path is empty
        if (IsNullOrWhiteSpace(filePath))
        {
            Debug.Log("File path is null or white space: " + filePath);
            // if so, return invalid data
            Metadata tempMeta = new Metadata();
            tempMeta.valid = false;
            return tempMeta;
        }

        // checks whether currently parsing the notes or other metadata
        bool _inNotes = false;

        Metadata songData = new Metadata();
        // if any major errors are encountered during parsing, this is set to
        // false and the song cannot be selected
        songData.valid = true;
        songData.noteDataExists = false;

        // colects raw data from the file all at once
        List<string> fileData = File.ReadAllLines(filePath).ToList();

        // get the file directory, and make sure it ends with either \\ or /
        string fileDir = Path.GetDirectoryName(filePath);
        if (!fileDir.EndsWith("\\") && !fileDir.EndsWith("/"))
        {
            fileDir += "\\";
        }

        // go through file data
        for (int i = 0; i < fileData.Count; i++)
        {
            // parse data from document
            string line = fileData[i].Trim();

            if (line.StartsWith("//"))
            {
                // it's a comment, ignore it and go to next line
                continue;
            }
            else if (line.StartsWith("#"))
            {
                // # denotes generic metadata for the song
                string key = line.Substring(0, line.IndexOf(':')).Trim('#').Trim(':');

                switch (key.ToUpper())
                {
                    case "TITLE":
                        songData.title = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "SUBTITLE":
                        songData.subtitle = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "ARTIST":
                        songData.artist = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "BANNER":
                        songData.bannerPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "BACKGROUND":
                        songData.backgroundPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "MUSIC":
                        songData.musicPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        if (IsNullOrWhiteSpace(songData.musicPath) || !File.Exists(songData.musicPath))
                        {
                            Debug.Log("No music file found.");
                            // no music file found
                            songData.musicPath = null;
                            songData.valid = false;
                        }
                        break;
                    case "OFFSET":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.offset))
                        {
                            // error parsing
                            songData.offset = 0.0f;
                        }
                        break;
                    case "SAMPLESTART":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.sampleStart))
                        {
                            // error parsing
                            songData.sampleStart = 0.0f;
                        }
                        break;
                    case "SAMPLELENGTH":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.sampleLength))
                        {
                            // error parsing
                            songData.sampleStart = sampleLengthDefault;
                        }
                        break;
                    case "BPM":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.bpm))
                        {
                            Debug.Log("BPM is not valid.");
                            // error parsing - BPM not valid
                            songData.valid = false;
                            songData.bpm = 0.0f;
                        }
                        Debug.Log("DISPLAYBPM: " + songData.bpm);
                        break;
                    case "NOTES":
                        _inNotes = true;
                        break;
                    default:
                        break;
                }

                // if now parsing note data
                if (_inNotes)
                {
                    // update the parsing for loop to after the current note chart
                    // also record note data along the way
                    // then analyse note data and parse further
                    List<string> noteChart = new List<string>();
                    for (int j = i; j < fileData.Count; j++)
                    {
                        string noteLine = fileData[j].Trim();
                        if (noteLine.EndsWith(";"))
                        {
                            i = j - 1;
                            break;
                        }
                        else
                        {
                            noteChart.Add(noteLine);
                        }
                    }

                    // begin parsing note data
                    songData.noteDataExists = true;
                    songData.noteData = ParseNotes(noteChart);
                }

                if (line.EndsWith(";"))
                {
                    _inNotes = false;
                }
            }
        }

        return songData;
    }

    // parse notes
    private NoteData ParseNotes(List<string> notes)
    {
        NoteData noteData = new NoteData();
        noteData.bars = new List<List<Notes>>();

        // work through each line of raw note data
        List<Notes> bar = new List<Notes>();
        for (int i = 0; i < notes.Count; i++)
        {
            // based on different line properties determine what data that line
            // contains
            // semicolon dictating end of note data
            // comma indicating end of bar
            string line = notes[i].Trim();

            if (line.StartsWith(";"))
            {
                break;
            }

            if (line.StartsWith(","))
            {
                noteData.bars.Add(bar);
                bar = new List<Notes>();
            }
            else if (line.EndsWith(":"))
            {
                continue;
            }
            else if (line.Length == 3)
            {
                // in single 'note row' such as '010'
                // check which column contains 'notes' and mark appropriate
                // flags
                Notes note = new Notes();
                note.top = 0;
                note.middle = 0;
                note.bottom = 0;

                if (line[0] != '0')
                {
                    // Debug.Log(line[0]);
                    // Debug.Log(Convert.ToByte(line[0]));
                    note.bottom = (NoteType)(Convert.ToByte(line[0]) - 48);
                    // Debug.Log(note.bottom.ToString());
                    // Debug.Log(note.bottom);
                }
                if (line[1] != '0')
                {
                    note.middle = (NoteType)(Convert.ToByte(line[1]) - 48);
                }
                if (line[2] != '0')
                {
                    note.top = (NoteType)(Convert.ToByte(line[2]) - 48);
                }

                // Debug.Log(note.top.ToString() + note.middle.ToString() + note.bottom.ToString());
                // add this information to current bar and continue until end
                bar.Add(note);
            }
        }

        return noteData;
    }

    // checks whether a string is null or whitespace
    public static bool IsNullOrWhiteSpace(string value)
    {
        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }
}

