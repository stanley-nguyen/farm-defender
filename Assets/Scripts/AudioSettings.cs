using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings instance { get; private set; }

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider audioSlider;

    [SerializeField]
    private Slider sfxSlider;

    private GameObject settingsPanel;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            settingsPanel = GameObject.Find("SettingsPanel");
            settingsPanel.SetActive(false);
        }
        float audioVal = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        audioMixer.SetFloat("music", Mathf.Log10(audioVal) * 20);
        audioSlider.value = audioVal;

        float sfxVal = PlayerPrefs.GetFloat("sfxVolume", 0.75f);
        audioMixer.SetFloat("music", Mathf.Log10(sfxVal) * 20);
        audioSlider.value = sfxVal;
    }

    public void SetMusicVolume()
    {
        float volume = audioSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) *20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
