using System;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public Sound[] sounds;

    void Awake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].audioSource = gameObject.AddComponent<AudioSource>();
            sounds[i].audioSource.clip = sounds[i].audioClip;
            sounds[i].audioSource.volume = sounds[i].volume;
            sounds[i].audioSource.pitch = sounds[i].pitch;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        sound.audioSource.Play();
    }
}