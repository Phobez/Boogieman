using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    //public Slider bgMusic;
    //public Slider sfx;

    public void SetMusicVolume(float musicValue)
    {
        float value = Mathf.Log10(musicValue) * 20;
        musicMixer.SetFloat("MusicVol", value);

        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SetSfxVolume(float sfxValue)
    {
        float value = Mathf.Log10(sfxValue) * 20;
        musicMixer.SetFloat("SfxVol", value);

        PlayerPrefs.SetFloat("SfxVol", value);
    }
}
