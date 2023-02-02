using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SoundAudioSource;
    [SerializeField] private AudioSource MusicAudioSource;

    private void Awake()
    {
        PublicVars.audioManager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName)
    {
        SoundAudioSource.PlayOneShot(PublicVars.gameResources.GetSound(soundName));
    }
}
