using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> music;

    private int currentMusic;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music[0];
        audioSource.Play();
        currentMusic = 0;
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            currentMusic++;
            currentMusic %= music.Count;
            audioSource.clip = music[currentMusic];
            audioSource.Play();
        }
    }
}
