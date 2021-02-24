using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particles;
    private AudioSource sound;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
        float time = particles.main.duration;
        if(sound != null)
        {
            time = Mathf.Max(time, sound.clip.length);
        }
        Destroy(gameObject, time);
    }
}
