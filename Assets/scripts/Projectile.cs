using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 bound;
    public int damage;
    public GameObject particlesPrefab;
    public Color particleColor;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb2d.position.y >= bound.y || rb2d.position.y <= -bound.y || rb2d.position.x >= bound.x || rb2d.position.x <= -bound.x)
        {
            Destroy(gameObject);
        }
    }

    public void TargetHit(bool showParticles)
    {
        if (showParticles)
        {
            GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            particles.GetComponent<Rigidbody2D>().velocity = rb2d.velocity;
            particles.SetActive(true);
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            if (gameObject.CompareTag("EnemyProjectile"))
            {
                particles.transform.localScale = new Vector3(1, 1, -1);
            }
            main.startColor = particleColor;
        }
        Destroy(gameObject);
    }

    public void TargetHit(int _)
    {
        Destroy(gameObject);
    }
}
