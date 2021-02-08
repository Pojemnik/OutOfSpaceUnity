using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public float shootCooldown;

    private float shootTimer = 0.0f;
    private Shooter shooter;
    void Start()
    {
        shooter = GetComponent<Shooter>();
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
            shooter.Shoot();
        }
    }
}
