using System;
using UnityEngine;

public class DiagonalAi : MonoBehaviour
{
    public float speed;
    public float range;
    public float direction;

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
        Vector2 pos = new Vector2((float)Math.Sin(time * speed) * direction, (float)Math.Sin(time * speed)) * range;
        rb2d.MovePosition(startPosition + pos);
        time += Time.fixedDeltaTime;
    }
}
