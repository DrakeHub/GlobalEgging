using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance { get; private set; } = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sounds soundclip in sounds)
        {
            soundclip.source = gameObject.AddComponent<AudioSource>();
            soundclip.source.clip = soundclip.clip;
            soundclip.source.volume = soundclip.volume;
            soundclip.source.pitch = soundclip.pitch;
            soundclip.source.loop = soundclip.loop;
        }
    }

    private void Start()
    {
        Play("Music");

    }

    public void Play (string name)
    {
        Sounds soundclip = Array.Find(sounds, sound => sound.name == name);
        if (soundclip == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        soundclip.source.Play();
    }

    public void Stop(string name)
    {
        Sounds soundclip = Array.Find(sounds, sound => sound.name == name);
        if (soundclip == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        soundclip.source.Stop();
    }
}
