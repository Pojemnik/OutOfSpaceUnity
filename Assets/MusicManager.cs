using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<AudioClip> music;

    private int currentMusic;
    private AudioSource audioSource;

    void Start()
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
        }
    }
}
