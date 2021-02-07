using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float upperBound;
    public float lowerBound;
    public int damage;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(rb2d.position.y >= upperBound || rb2d.position.y <= lowerBound)
        {
            Destroy(gameObject);
        }
    }

    public void TargetHit()
    {
        Destroy(gameObject);
    }
}
