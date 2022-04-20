using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
///
///</summary>
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer sfxAudioMixer;
    public AudioSource clickSound;
    public TMP_Dropdown resDropdown;
    
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i =0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " "+ resolutions[i].refreshRate+"Hz";
            options.Add(option);
            
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();


        Button[] btn = FindObjectsOfType<Button>();
        foreach (Button b in btn)
        {
            b.onClick.AddListener(Onclick);
        }
    }
    void Onclick()
    {
        clickSound.Play();
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }    
    
    public void SetSFXVolume(float volume)
    {
        sfxAudioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    
    public void ButtonClick()
    {
        clickSound.Play();
    }
}
