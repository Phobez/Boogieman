using System.IO;
using UnityEngine;

//  Made by     : Abia Herlianto
//  Description : A class to handle initial processing in the main menu.
public class MainMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // loads song directory
        // creates it if it doesn't exist
        string _myDocumentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        string _songDirectory = _myDocumentsPath + "\\Melody of Nature\\Songs";

        if (!Directory.Exists(_songDirectory))
        {
            Directory.CreateDirectory(_songDirectory);
        }

        GameData.songDirectory = _songDirectory;

        // loads saved data
        if (GameData.currentSavedData == null)
        {
            SaveLoad.Load();
        }
        else
        {
            SaveLoad.Save();
        }
        Debug.Log(GameData.currentSavedData.highscores.Count);
    }
}
