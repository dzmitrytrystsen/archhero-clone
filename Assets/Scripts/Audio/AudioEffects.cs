using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NameOfSoundEffect
{
    ProjectileEffect,
    ProjectileImpact
}

public class AudioEffects : Singleton<AudioEffects>
{
    [Serializable]
    public struct AudioEffect
    {
        public NameOfSoundEffect Name;
        public List<AudioClip> SoundEffects;
    }


    [SerializeField] private List<AudioEffect> _audioEffects;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlaySoundEffect(NameOfSoundEffect nameOfSoundEffect, AudioSource audioSource)
    {
        AudioClip audioClipToPlay;

        foreach (AudioEffect audio in _audioEffects)
        {
            if (audio.Name == nameOfSoundEffect)
            {
                audioClipToPlay = audio.SoundEffects[Random.Range(0, audio.SoundEffects.Count)];
                audioSource.PlayOneShot(audioClipToPlay);
            }
        }
    }
}
