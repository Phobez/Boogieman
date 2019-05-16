using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// A class to write the song data into an .mn file.
/// </summary>
public class SongWriter
{
    /// <summary>
    /// Writes the .mn file using the provided song data.
    /// </summary>
    /// <param name="songData">Song data to write the notes to.</param>
    /// <param name="targetPath">Target path to write the song to.</param>
    public void Write(SongParser.Metadata songData, string targetPath)
    {
        List<string> _fileData = new List<string>();

        string _currLine = "";
        // write title
        _currLine = "#TITLE:" + songData.title + ";";
        _fileData.Add(_currLine);

        // write subtitle
        _currLine = "#SUBTITLE:" + songData.subtitle + ";";
        _fileData.Add(_currLine);

        // write artist
        _currLine = "#ARTIST:" + songData.artist + ";";
        _fileData.Add(_currLine);

        // write banner path
        _currLine = "#BANNER:" + songData.bannerPath + ";";
        _fileData.Add(_currLine);

        // write background path
        _currLine = "#BACKGROUND:" + songData.backgroundPath + ";";
        _fileData.Add(_currLine);

        // write music path
        _currLine = "#MUSIC:" + songData.musicPath + ";";
        _fileData.Add(_currLine);

        // write offset
        _currLine = "#OFFSET:" + songData.offset + ";";
        _fileData.Add(_currLine);

        // write sample start
        _currLine = "#SAMPLESTART:" + songData.sampleStart + ";";
        _fileData.Add(_currLine);

        // write sample length
        _currLine = "#SAMPLELENGTH:" + songData.sampleLength + ";";
        _fileData.Add(_currLine);

        // write bpm
        _currLine = "#BPM:" + songData.bpm + ";";
        _fileData.Add(_currLine);

        _currLine = Environment.NewLine;
        _fileData.Add(_currLine);

        // write notes
        List<string> _fileDataNotes = WriteNotes(songData.noteData);

        _fileData.AddRange(_fileDataNotes);

        targetPath = targetPath + "/" + songData.musicPath.Remove(songData.musicPath.Length - 4) + ".mn";

        File.WriteAllLines(targetPath, _fileData.ToArray());
    }

    /// <summary>
    /// Converts note data into .mn file-compatible format.
    /// </summary>
    /// <param name="noteData">The note data to convert.</param>
    /// <returns>Converted note data.</returns>
    private List<string> WriteNotes(SongParser.NoteData noteData)
    {
        List<string> _fileDataNotes = new List<string>();

        string _currLine = "";

        _currLine = "#NOTES:";
        _fileDataNotes.Add(_currLine);

        int _barCounter = 1;

        foreach (List<SongParser.Notes> bar in noteData.bars)
        {
            _currLine = "// measure " + _barCounter;
            _fileDataNotes.Add(_currLine);

            foreach (SongParser.Notes note in bar)
            {
                // _currLine = note.bottom.ToString() + note.middle.ToString() + note.top.ToString();
                _currLine = (Convert.ToByte(note.bottom)).ToString() + (Convert.ToByte(note.middle)).ToString() + (Convert.ToByte(note.top)).ToString();
                _fileDataNotes.Add(_currLine);
            }

            _currLine = ",";
            _fileDataNotes.Add(_currLine);

            _barCounter++;
        }

        _fileDataNotes[_fileDataNotes.Count - 1] = ";";

        return _fileDataNotes;
    }
}
