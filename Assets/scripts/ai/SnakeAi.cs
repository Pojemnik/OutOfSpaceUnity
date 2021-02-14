using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeAi : MonoBehaviour
{
    public float speed;
    public List<DirectionBound> moveSquence;

    private Rigidbody2D rb2d;
    private DirectionBound currentSequenceElement;
    private int sequenceIterator;
    private Vector2 currentElementStartPos;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentSequenceElement = moveSquence[0];
        sequenceIterator = 0;
        currentElementStartPos = rb2d.position;
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        bool onBounds = false;
        Vector2 currentBounds = currentSequenceElement.bound;
        if (currentSequenceElement.global)
        {
            if (currentSequenceElement.direction.x > 0 && position.x > currentBounds.x)
            {
                rb2d.MovePosition(new Vector2(currentBounds.x, position.y));
                position.x = currentBounds.x;
                onBounds = true;
            }
            if (currentSequenceElement.direction.x < 0 && position.x < -currentBounds.x)
            {
                rb2d.MovePosition(new Vector2(-currentBounds.x, position.y));
                onBounds = true;
            }
            if (currentSequenceElement.direction.y > 0 && position.y > currentBounds.y)
            {
                rb2d.MovePosition(new Vector2(position.x, currentBounds.y));
                onBounds = true;
            }
            if (currentSequenceElement.direction.y < 0 && position.y < -currentBounds.y)
            {
                rb2d.MovePosition(new Vector2(position.x, -currentBounds.y));
                onBounds = true;
            }
        }
        else
        {
            currentBounds += currentElementStartPos;
            if (currentSequenceElement.direction.x > 0 && position.x > currentBounds.x)
            {
                rb2d.MovePosition(new Vector2(currentBounds.x, position.y));
                position.x = currentBounds.x;
                onBounds = true;
            }
            if (currentSequenceElement.direction.x < 0 && position.x < currentBounds.x)
            {
                rb2d.MovePosition(new Vector2(currentBounds.x, position.y));
                onBounds = true;
            }
            if (currentSequenceElement.direction.y > 0 && position.y > currentBounds.y)
            {
                rb2d.MovePosition(new Vector2(position.x, currentBounds.y));
                onBounds = true;
            }
            if (currentSequenceElement.direction.y < 0 && position.y < currentBounds.y)
            {
                rb2d.MovePosition(new Vector2(position.x, currentBounds.y));
                onBounds = true;
            }
        }
        if (onBounds)
        {
            sequenceIterator++;
            currentSequenceElement = moveSquence[sequenceIterator % moveSquence.Count];
            currentElementStartPos = rb2d.position;
            return;
        }
        rb2d.MovePosition(position + (Time.fixedDeltaTime * speed * (Vector2)currentSequenceElement.direction));
    }

    public struct DirectionBound
    {
        public Vector2Int direction { get; }
        public Vector2 bound { get; }
        public bool global { get; }

        public DirectionBound(Vector2Int direction_, Vector2 bound_, bool global_)
        {
            direction = direction_;
            bound = bound_;
            global = global_;
        }
    }
}
