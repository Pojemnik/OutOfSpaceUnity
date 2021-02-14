using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAi : MonoBehaviour
{
    public List<Vector2> path;
    public float speed;

    private Vector2 nextPoint;
    private Vector2 currentPoint;
    private Rigidbody2D rb2d;
    private float timeLeft;
    private float maxTime;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentPoint = rb2d.position;
        nextPoint = path[0];
        float distanceToNextPoint = Vector2.Distance(nextPoint, currentPoint);
        timeLeft = distanceToNextPoint / speed;
        maxTime = timeLeft;
    }

    void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        float time = timeLeft / maxTime;
        rb2d.MovePosition(Vector2.Lerp(currentPoint, nextPoint, time));
    }
}
