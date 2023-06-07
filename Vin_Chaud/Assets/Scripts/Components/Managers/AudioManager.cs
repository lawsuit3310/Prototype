using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource _audioSource;
    private static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
