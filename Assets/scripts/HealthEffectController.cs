using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffectController : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent playerHealed;
    public UnityEngine.Events.UnityEvent updatePlayerHealth;

    private ParticleSystem particles;
    private AudioSource sound;
    private float particleTime;
    private float soundTime;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
        particleTime = particles.main.duration;
        soundTime = sound.clip.length;
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(particleTime);
        updatePlayerHealth.Invoke();
        yield return new WaitForSeconds(soundTime - particleTime);
        playerHealed.Invoke();
        Destroy(gameObject);
    }
}
