using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public float shootCooldown;

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
            shootTimer += shootCooldown;
            foreach (Shooter shooter in shooters)
            {
                shooter.Shoot();
            }
        }
    }
}
