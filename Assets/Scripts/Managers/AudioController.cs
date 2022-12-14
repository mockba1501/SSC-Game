using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;
    public AudioSource musicSource;
    public AudioSource sfxForBtns;

    public AudioClip clickAudio;

    //audios
    public AudioSource mainMenu;
    public AudioSource cancelAudio;
    public AudioSource fixBuildingAudio;
    public AudioSource nextTurnAudio;
    public AudioSource placeBuildingAudio;
    public AudioSource cantPlaceBuildingAudio;
    public AudioSource nextLevelAudio;
    public AudioSource backrgroundAudio;
    public AudioSource levelFailedAudio;

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

    public void PlaceBuildingPlay()
    {
        placeBuildingAudio.Play();
    }

    public void CantPlaceBuildingPlay()
    {
        cantPlaceBuildingAudio.Play();
    }

    public void MainMenuPlay()
    {
        mainMenu.Play();
    }

    public void CancelButtonPlay()
    {
        cancelAudio.Play();
    }
    public void FixBuildingPlay()
    {
        fixBuildingAudio.Play();
    }

    public void NextTurnPlay()
    {
        nextTurnAudio.Play();
    }

    public void NextLevelPlay()
    {
        nextLevelAudio.Play();
    }

    public void LevelBackgroundStop()
    {
     backrgroundAudio.Stop();   
    }

    public void LevelFailedPlay()
    {
        levelFailedAudio.Play();
    }
}
