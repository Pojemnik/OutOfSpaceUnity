using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 bound;
    public int damage;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(rb2d.position.y >= bound.y || rb2d.position.y <= -bound.y || rb2d.position.x >= bound.x || rb2d.position.x <= -bound.x)
        {
            Destroy(gameObject);
        }
    }

    public void TargetHit()
    {
        Destroy(gameObject);
    }

    public void TargetHit(int _)
    {
        Destroy(gameObject);
    }
}
