using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    [Range(0f,2f)]
    public float volume;
    [Range(.5f, 1.5f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
