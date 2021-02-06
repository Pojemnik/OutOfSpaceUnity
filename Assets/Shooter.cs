using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float shootCooldown;
    public Vector2 projectileOffset;

    private float shootTimer = 0.0f;

    void FixedUpdate()
    {
        if (shootTimer >= 0.0f)
        {
            shootTimer -= Time.fixedDeltaTime;
        }
    }

    public void Shoot()
    {
        if (shootTimer <= 0.0f)
        {
            Vector3 projectilePosition = transform.position + new Vector3(projectileOffset.x, projectileOffset.y);
            GameObject newProjectile = Instantiate(projectilePrefab, projectilePosition, transform.rotation);
            Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
            projectileRB.velocity = new Vector2(0, projectileSpeed);
            shootTimer += shootCooldown;
        }
    }
}
