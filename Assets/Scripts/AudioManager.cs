using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake()
    {
        instance = this;
    }


    public AudioClip sfx_Landing, sfx_Cherry;
    public AudioClip music_TickTok;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch(sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_Landing);
                break;
            case "cherry":
                SoundObjectCreation(sfx_Cherry);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();

    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "ticktok":
                MusicObjectCreation(music_TickTok);
                break;
            default:
                break;
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().loop = true;
        newObject.GetComponent<AudioSource>().Play();

    }
}
