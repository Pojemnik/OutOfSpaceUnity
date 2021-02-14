using UnityEngine;
using System;

public class EightAi : MonoBehaviour
{
    public float speed;

    private float time = 0.0f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2((float)Math.Cos(time * speed), (float)Math.Cos(time * speed * 2));
        rb2d.velocity = pos;
        time += Time.fixedDeltaTime;
    }
}
