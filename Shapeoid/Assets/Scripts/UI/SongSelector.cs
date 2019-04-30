using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

// Made by      : Abia Herlianto
// Description  : Handles the reading of songs from the proper directory as
//                well as the selection of the songs
public class SongSelector : MonoBehaviour
{
    public GameObject songSelectionPrefab;
    public GameObject songSelectionList;

    private AudioSource audioSource;

    private string currentSongPath;

    private float audioStartTime;
    private float audioLength;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Parse();
    }

    // a method to parse all song files in directory
    private void Parse()
    {
        Debug.Log("Parsing.");
        DirectoryInfo _info = new DirectoryInfo(GameData.songDirectory);
        FileInfo[] _mnFiles = _info.GetFiles("*.mn", SearchOption.AllDirectories);
        Debug.Log("Parsing Directory: " + GameData.songDirectory + " | Amount: " + _mnFiles.Length);

        for (int i = 0; i < _mnFiles.Length; i++)
        {
            SongParser _parser = new SongParser();
            Debug.Log("Full name: " + _mnFiles[i].FullName);
            SongParser.Metadata _songData = _parser.Parse(_mnFiles[i].FullName);

            audioStartTime = _songData.sampleStart;
            audioLength = _songData.sampleLength;

            if (!_songData.valid)
            {
                Debug.Log("Song data is not valid.");
                // song data isn't valid
                continue;
            }
            else
            {
                GameObject _songObj = (GameObject)Instantiate(songSelectionPrefab, songSelectionList.transform.position, Quaternion.identity);
                _songObj.GetComponentInChildren<Text>().text = _songData.title + " - " + _songData.artist;
                // _songObj.transform.parent = songSelectionList.transform;
                _songObj.transform.SetParent(songSelectionList.transform, false);
                _songObj.transform.localScale = new Vector3(1, 1, 1); // reset scale just in case scale changes

                // get access to button control
                Button _songButton = _songObj.GetComponentInChildren<Button>();
                if (File.Exists(_songData.bannerPath))
                {
                    Texture2D texture = new Texture2D(766, 182);
                    texture.LoadImage(File.ReadAllBytes(_songData.bannerPath));
                    Debug.Log(_songData.bannerPath);
                    _songButton.image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                }

                _songButton.onClick.AddListener(delegate { StartSong(_songData); });

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener(eventData => { if (_songData.musicPath != currentSongPath) { StartCoroutine(PreviewTrack(_songData.musicPath)); } });

                _songButton.GetComponent<EventTrigger>().triggers.Add(entry);
            }
        }
    }

    // a method to play a preview of the currently hovered-over track
    private IEnumerator PreviewTrack(string musicPath)
    {
        Debug.Log("Starting preview for " + musicPath);
        string _url = string.Format("file://{0}", musicPath);
        //UnityWebRequest _unityWebRequest = new UnityWebRequest(_url);

        //while (!_unityWebRequest.isDone)
        //{
        //    yield return null;
        //}

        //_unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(_url, AudioType.MPEG);
        //audioSource.clip = DownloadHandlerAudioClip.GetContent(_unityWebRequest);

        WWW _www = new WWW(_url);

        while (!_www.isDone)
        {
            yield return null;
        }

        AudioClip _clip = NAudioPlayer.FromMp3Data(_www.bytes);
        audioSource.clip = _clip;

        Debug.Log("Loaded.");

        audioSource.Play();
        audioSource.time = audioStartTime;

        currentSongPath = musicPath;

        // audioSource.volume = 0;
    }

    // transitions from song selection to in-game
    private void StartSong(SongParser.Metadata songData)
    {
        Debug.Log(songData.title + " chosen.");
        GameData.chosenSongData = songData;
        SceneManager.LoadScene("InGame");
    }
}
