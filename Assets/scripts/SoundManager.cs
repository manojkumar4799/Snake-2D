using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public SetSound[] setSound;
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    static SoundManager soundManager;
    public static SoundManager Instance{ get { return soundManager; } }


    private void Awake()
    {
        if (soundManager== null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Main")
        {
            PlayBGM(Sound.BGM);
        }
        else
        {
            PlayBGM(Sound.Intro);
        }
    }

    public void PlayBGM(Sound sound)
    {
        AudioClip audioClip;
        SetSound findSound = Array.Find(setSound, findSound => findSound.audioType == sound);
        if (findSound != null)
        {
            audioClip = findSound.audio;
            BGMSource.clip = audioClip;
            if (findSound.audioType == Sound.GameOver)
            {
                BGMSource.loop = false;
            }
            else
            {
                BGMSource.loop = true;
            }
            BGMSource.Play();
        }
        else { return; }
        
    }

    public void PlaySound(Sound sound)
    {
      
        SetSound getSound = Array.Find(setSound, findSound => findSound.audioType == sound);
        if(getSound!=null)
        {
            AudioClip audio = getSound.audio;
            SFXSource.PlayOneShot(audio);
        }
        else
        {
            return;
        }
        
        
    }
}

[Serializable]
public class SetSound
{
    public AudioClip audio;
    public Sound audioType;
}

public enum Sound
{
    Intro,
    BGM,
    Eat,
    GameOver,
    Teleport
}
