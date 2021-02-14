using System;
using UnityEngine;

public class CircleAi : MonoBehaviour
{
    public float speed;
    public float range;

    private float time = 0.0f;
    private Rigidbody2D rb2d;
    private Vector2 startPosition;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = rb2d.position;
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2((float)Math.Cos(time * speed), (float)Math.Sin(time * speed)) * range;
        rb2d.MovePosition(startPosition + pos);
        time += Time.fixedDeltaTime;
    }
}
