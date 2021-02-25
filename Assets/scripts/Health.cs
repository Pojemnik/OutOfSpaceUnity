using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startHealth;
    public entityType type;
    public UnityEvent deathEvent;
    public GameObject explosion;
    public Vector2 explosionOffset;
    public Vector2 hpBarOffset;
    public AudioClip damageClip;
    public UnityEngine.Audio.AudioMixerGroup damageGroup;

    private AudioSource audioSource;

    private int currentHealth;

    void Awake()
    {
        currentHealth = startHealth;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach(AudioSource source in audioSources)
        {
            if(source.outputAudioMixerGroup == damageGroup)
            {
                audioSource = source;
                break;
            }
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        BroadcastMessage("Damaged", currentHealth);
        audioSource.PlayOneShot(damageClip);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Healed()
    {
        currentHealth += 1;
        if(currentHealth > startHealth)
        {
            currentHealth = startHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        bool enemyShotByPlayer = (type == entityType.Enemy && other.CompareTag("Projectile"));
        bool playerShotByEnemy = (type == entityType.Player && other.CompareTag("EnemyProjectile"));
        if (enemyShotByPlayer || playerShotByEnemy)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (currentHealth - projectile.damage > 0)
            {
                projectile.TargetHit(true);
            }
            else
            {
                projectile.TargetHit(false);
            }
            Hit(projectile.damage);
        }
    }

    public void Die()
    {
        deathEvent.Invoke();
        Vector2 explosionPosition = (Vector2)transform.position + explosionOffset;
        Instantiate(explosion, explosionPosition, transform.rotation).SetActive(true);
        Destroy(gameObject);
    }

    public enum entityType
    {
        Enemy,
        Player
    }
}
