﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// Made by      : Abia Herlianto
// Description  : loads and plays the song and manages scene change.
public class SongController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool songLoaded = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        SongParser.Metadata _metadata = GameData.chosenSongData;

        StartCoroutine(LoadTrack(_metadata.musicPath, _metadata));
    }

    // Update is called once per frame
    private void Update()
    {
        if (!audioSource.isPlaying && songLoaded)
        {
            // song over
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    // loads and plays song
    private IEnumerator LoadTrack(string path, SongParser.Metadata _metadata)
    {
        Debug.Log(path);
        string _url = string.Format("file://{0}", path);
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

        songLoaded = true;

        audioSource.Play();

        GameObject _controller = GameObject.FindGameObjectWithTag("GameController");
        _controller.GetComponent<NoteGenerator>().InitNotes(_metadata);
    }
}
