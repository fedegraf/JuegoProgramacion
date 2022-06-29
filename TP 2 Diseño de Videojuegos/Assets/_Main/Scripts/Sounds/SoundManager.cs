using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, x => x.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound is Null");
            return;
        }

        source.clip = s.audioClip;
        source.loop = s.isLooping;
        source.Play();
    }

    public void StopSound(string soundName)
    {
        Sound s = Array.Find(sounds, x => x.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound is Null");
            return;
        }

        source.Stop();
    }
}

[Serializable]
public class Sound
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public bool isLooping;
}
