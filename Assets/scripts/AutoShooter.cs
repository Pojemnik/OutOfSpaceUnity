using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public float shootCooldown;
    public float cooldownVariation;

    private float shootTimer = 0.0f;
    private Shooter[] shooters;
    void Start()
    {
        shooters = GetComponents<Shooter>();
        shootTimer += Random.Range(0, shootCooldown);
    }

    void FixedUpdate()
    {
        if (shootTimer >= 0.0f)
        {
            shootTimer -= Time.fixedDeltaTime;
        }
        else
        {
            shootTimer += shootCooldown + Random.Range(-cooldownVariation, cooldownVariation);
            foreach (Shooter shooter in shooters)
            {
                shooter.Shoot();
            }
        }
    }
}
