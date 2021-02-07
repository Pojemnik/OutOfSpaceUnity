using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startHealth;
    public entityType type;
    public UnityEvent deathEvent;

    private int currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        bool enemyShotByPlayer = (type == entityType.Enemy && other.CompareTag("Projectile"));
        bool playerShotByEnemy = (type == entityType.Player && other.CompareTag("EnemyProjectile"));
        if (enemyShotByPlayer || playerShotByEnemy)
        {
            Projectile projectile = other.GetComponent<Projectile>();
            currentHealth -= projectile.damage;
            if(currentHealth <= 0)
            {
                Die();
            }
            projectile.TargetHit();
            BroadcastMessage("Damaged", projectile.damage);
        }
    }

    private void Die()
    {
        deathEvent.Invoke();
        Destroy(gameObject);
    }

    public enum entityType
    {
        Enemy,
        Player
    }
}
