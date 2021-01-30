using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float xBound;
    public float defaultY;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if ((moveHorizontal > 0 && rb2d.position.x < xBound) || (moveHorizontal < 0 && rb2d.position.x > -xBound))
        {
            rb2d.MovePosition(new Vector2(rb2d.position.x + (moveHorizontal * speed * Time.fixedDeltaTime), defaultY));
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }
}
