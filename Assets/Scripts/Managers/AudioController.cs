using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;
    public AudioSource musicSource;
    public AudioSource sfxForBtns;

    public AudioClip clickAudio;
    private void Awake()
    {
        if(audioController == null)
        {
            audioController = this;
        }
    }
    public void PlayClickAudio()
    {
        sfxForBtns.PlayOneShot(clickAudio);
    }


    public void SetMusiceAudioSourcesVlolume()
    {
        musicSource.volume = PlayerPrefs.GetFloat(UISettingContontoller.music);
    }
    public void SetSFXAudioSourcesVlolume()
    {
        sfxForBtns.volume = PlayerPrefs.GetFloat(UISettingContontoller.sfx);
    }

}
