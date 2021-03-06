﻿using NishiKata.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NishiKata.Audio
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        public AudioMixerGroup audioMixerGroup;
        public Sound[] sounds;

        private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

        protected override void Awake()
        {
            base.Awake();
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
                sounds[i].audioSource.loop = sounds[i].loop;
                sounds[i].audioSource.outputAudioMixerGroup = audioMixerGroup;
                sounds[i].dontStackSounds = sounds[i].dontStackSounds;
                soundDict[sounds[i].name] = sounds[i];
            }
        }

        public void Play(string name)
        {
            Sound sound;

            if (!soundDict.TryGetValue(name, out sound))
            {
                DebugExtensions.LogNotFound(this, name);
                return;
            }

            if (sound.dontStackSounds && sound.audioSource.isPlaying)
            {
                return;
            }

            sound.audioSource.Play();
        }

        public void PlaySong(string name)
        {
            Sound song;

            if (!soundDict.TryGetValue(name, out song))
            {
                DebugExtensions.LogNotFound(this, name);
                return;
            }

            if (!song.audioSource.isPlaying)
            {
                song.audioSource.Play();
            }
        }

        public void StopSong(string name)
        {
            Sound song;

            if (!soundDict.TryGetValue(name, out song))
            {
                DebugExtensions.LogNotFound(this, name);
                return;
            }

            if (song.audioSource.isPlaying)
            {
                song.audioSource.Stop();
            }
        }

        public void PauseSong(string name)
        {
            Sound song;

            if (!soundDict.TryGetValue(name, out song))
            {
                DebugExtensions.LogNotFound(this, name);
                return;
            }

            if (song.audioSource.isPlaying)
            {
                song.audioSource.Pause();
            }
        }

        public string GetCurrentlyPlayingSongName()
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].audioSource.isPlaying &&
                    sounds[i].loop)
                {
                    return sounds[i].name;
                }
            }

            return string.Empty;
        }
    }
}