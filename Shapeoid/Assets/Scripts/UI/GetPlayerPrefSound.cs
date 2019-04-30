using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class GetPlayerPrefSound : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            // Update volMusic volume slider from PlayerPrefs
            musicMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));

            float currentVolMusic = PlayerPrefs.GetFloat("MusicVol");

            Debug.Log(currentVolMusic);
        }

        if (PlayerPrefs.HasKey("SfxVol"))
        {
            // Update volSoundFx volume slider from PlayerPrefs
            sfxMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
        }
    }
}
