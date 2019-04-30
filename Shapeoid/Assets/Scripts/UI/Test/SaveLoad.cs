using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

/// <summary>
/// A static class which handles saving and loading saved data.
/// </summary>
public static class SaveLoad
{
    // saves saved data as a binary-serialised file
    public static void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat");
        binaryFormatter.Serialize(file, GameData.currentSavedData);
        file.Close();
    }

    // loads saved data
    // creates new saved data if it doesn't exist
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);
            GameData.currentSavedData = (SavedData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        else
        {
            GameData.currentSavedData = new SavedData();
            SaveLoad.Save();
        }
    }
}
