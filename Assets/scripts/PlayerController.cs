using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 bounds;
    public float shootCooldown;
    public GameObject jumpPortalPrefab;
    public Vector2 portalOffset;
    public UnityEngine.Audio.AudioMixerGroup playerAlarmGroup;
    public AudioClip alarmClip;
    public UnityEngine.Events.UnityEvent damagedEvent;

    private Rigidbody2D rb2d;
    private Shooter shooter;
    private Health health;
    private float shootTimer = 0.0f;
    private AudioSource alarmSource;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
        var sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.outputAudioMixerGroup == playerAlarmGroup)
            {
                alarmSource = source;
                break;
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.Hit(1);
        }
    }

    public void Damaged(int currentHealth)
    {
        if (currentHealth != 0)
        {
            damagedEvent.Invoke();
            alarmSource.PlayOneShot(alarmClip);
        }
    }

    public void OnJumpStart()
    {
        Vector2 portalPosition = (Vector2)transform.position + portalOffset;
        Instantiate(jumpPortalPrefab, portalPosition, transform.rotation).SetActive(true);
        gameObject.SetActive(false);
    }
}
