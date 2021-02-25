using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthPickupController : MonoBehaviour
{
    public float minY;
    public GameObject healEffect;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d.position.y > minY)
        {
            StartCoroutine(positionCheckCoroutine());
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    private IEnumerator positionCheckCoroutine()
    {
        while (rb2d.position.y > minY)
        {
            yield return new WaitForSeconds(1);
        }
        rb2d.velocity = new Vector2(0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(healEffect, collision.gameObject.transform).SetActive(true);
            Destroy(gameObject);
        }
    }
}
