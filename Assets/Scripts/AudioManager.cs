using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager instance;
    public Sound[] sounds;

    private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

    void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        CreateAudioSources();
    }

    void CreateAudioSources()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].audioSource = gameObject.AddComponent<AudioSource>();
            sounds[i].audioSource.clip = sounds[i].audioClip;
            sounds[i].audioSource.volume = sounds[i].volume;
            sounds[i].audioSource.pitch = sounds[i].pitch;
            soundDict[sounds[i].name] = sounds[i];
        }
    }

    public void Play(string name)
    {
        Sound sound;

        if (!soundDict.TryGetValue(name, out sound))
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        sound.audioSource.Play();
    }
}