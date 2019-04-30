using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// Made by  : Kevin Trisnadi, Abia Herlianto
/// <summary>
/// A component which converts the volume slider value and sets the volume.
/// </summary>
public class VolumeSetter : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 0.0f);
        }

        if (!PlayerPrefs.HasKey("SFXVol"))
        {
            PlayerPrefs.SetFloat("SFXVol", 0.0f);
        }
    }

    // sets the slider value to the current value
    private void OnEnable()
    {
        musicSlider.value = Mathf.Pow(10.0f, PlayerPrefs.GetFloat("MusicVol") / 20.0f);
        sfxSlider.value = Mathf.Pow(10.0f, PlayerPrefs.GetFloat("SFXVol") / 20.0f);
    }

    public void SetMusicVolume()
    {
        float value = Mathf.Log10(musicSlider.value) * 20;
        musicMixer.SetFloat("MusicVol", value);

        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SetSFXVolume()
    {
        float value = Mathf.Log10(sfxSlider.value) * 20;
        sfxMixer.SetFloat("SFXVol", value);

        PlayerPrefs.SetFloat("SFXVol", value);
    }
}
