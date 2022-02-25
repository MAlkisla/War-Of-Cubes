using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
    [SerializeField]
    private AudioSource backgroundMusic, buttonClickSound, levelCompletedSound;

    [HideInInspector]
    public bool soundIsOn = true;

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayBackgroundMusic()
    {
        if (soundIsOn)
            backgroundMusic.Play();
    }
    
    public void ButtonClickSound()
    {
        if (soundIsOn)
            buttonClickSound.Play();
    }

    public void LevelCompletedSound()
    {
        if (soundIsOn)
            levelCompletedSound.Play();
    }
}
