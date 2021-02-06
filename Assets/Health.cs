using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth;

    private int currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {

        }
    }
}
