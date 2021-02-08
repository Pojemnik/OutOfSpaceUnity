using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 bounds;
    public float shootCooldown;

    private Rigidbody2D rb2d;
    private Shooter shooter;
    private float shootTimer = 0.0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && shootTimer <= 0.0f)
        {
            shooter.Shoot();
            shootTimer += shootCooldown;
        }
    }
    void FixedUpdate()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 move = rb2d.position;

        if (shootTimer >= 0.0f)
        {
            shootTimer -= Time.fixedDeltaTime;
        }

        if ((moveInput.x > 0 && rb2d.position.x < bounds.x) || (moveInput.x < 0 && rb2d.position.x > -bounds.x))
        {
            move.x += moveInput.x * speed.x * Time.fixedDeltaTime;
        }
        if ((moveInput.y > 0 && rb2d.position.y < bounds.y) || (moveInput.y < 0 && rb2d.position.y > -bounds.y))
        {
            move.y += moveInput.y * speed.y * Time.fixedDeltaTime;
        }
        rb2d.MovePosition(move);
    }
}
