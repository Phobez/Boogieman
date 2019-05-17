using System.Collections.Generic;
using UnityEngine;

//  Made by     : Abia Herlianto
//  Description : A static class that holds persistent game data:
//                song directory, currently chosen song, and current key bindings.
public static class GameData
{
    public static string songDirectory = "";
    public static SongParser.Metadata chosenSongData;
    public static SavedData currentSavedData;
    public static SongStatsHandler.SongStats currentSongStats;
}

/// <summary>
/// A class which contains saved data.
/// </summary>
[System.Serializable]
public class SavedData
{
    public Dictionary<string, KeyCode> keyBindings;

    public SavedData()
    {
        keyBindings = new Dictionary<string, KeyCode>();

        keyBindings.Add("FireKey", KeyCode.W);
        keyBindings.Add("AirKey", KeyCode.A);
        keyBindings.Add("WaterKey", KeyCode.D);
        keyBindings.Add("EarthKey", KeyCode.S);
    }
}