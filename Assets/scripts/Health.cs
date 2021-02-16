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

    private int currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        BroadcastMessage("Damaged", currentHealth);
        if (currentHealth <= 0)
        {
            Die();
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
            projectile.TargetHit();
            Hit(projectile.damage);
        }
    }

    private void Die()
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
