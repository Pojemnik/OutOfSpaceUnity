using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public float startCooldown;
    public bool alternativeMode = false;
    public float shootCooldown
    {
        set
        {
            if (cooldown > value)
            {
                shootTimer = -float.Epsilon;
                cooldown = value;
            }
            else if(cooldown < value)
            {
                cooldown = value;
                shootTimer = cooldown;
            }
        }
        get { return cooldown; }
    }
    public float cooldownVariation;

    private float cooldown;
    private float shootTimer = 0.0f;
    private Shooter[] shooters;

    void Awake()
    {
        cooldown = startCooldown;
        shooters = GetComponents<Shooter>();
        shootTimer += Random.Range(0, cooldown);
    }

    void FixedUpdate()
    {
        if (shootTimer >= 0.0f)
        {
            shootTimer -= Time.fixedDeltaTime;
        }
        else
        {
            shootTimer += cooldown + Random.Range(-cooldownVariation, cooldownVariation);
            if (alternativeMode)
            {
                shooters[Random.Range(0, shooters.Length)].Shoot();
            }
            else
            {
                foreach (Shooter shooter in shooters)
                {
                    shooter.Shoot();
                }
            }
        }
    }
}
