using System;
using UnityEngine;

public class ManeuverAi1 : MonoBehaviour
{
    public Vector2 arcSpeed;
    public int startDirection;
    public float upDownSpeed;
    public float swipeSpeed;

    private float time = 0;
    private Rigidbody2D rb2d;
    private Vector2 startPosition;
    private StateEnum state = StateEnum.ArcLeft;
    private float stateStartTime = 0;
    private float[] durations =
    {
        (float)(Math.PI), (float)(Math.PI), (float)(Math.PI/2), (float)(Math.PI/2), (float)(Math.PI/2), (float)(Math.PI/2)
    };
    private StateEnum[] transitions =
    {
        StateEnum.Down,         //AL
        StateEnum.Up,           //AR
        StateEnum.SwipeRight,   //D
        StateEnum.SwipeLeft,    //U
        StateEnum.ArcLeft,      //SL
        StateEnum.ArcRight      //SR
    };

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = rb2d.position;
    }

    private void FixedUpdate()
    {
        float duration = time - stateStartTime;
        Vector2 pos;
        switch (state)
        {
            case StateEnum.ArcLeft:
                pos = new Vector2((float)-Math.Sin(duration / 2), (float)Math.Sin(Math.PI + duration)) * arcSpeed;
                break;
            case StateEnum.ArcRight:
                pos = new Vector2((float)Math.Sin(duration / 2), (float)Math.Sin(Math.PI + duration)) * arcSpeed;
                break;
            case StateEnum.SwipeLeft:
                pos = new Vector2((float)-Math.Sin(duration) * swipeSpeed, 0.0f);
                break;
            case StateEnum.SwipeRight:
                pos = new Vector2((float)Math.Sin(duration) * swipeSpeed, 0.0f);
                break;
            case StateEnum.Up:
                pos = new Vector2(0.0f, (float)Math.Sin(duration) * swipeSpeed);
                break;
            case StateEnum.Down:
                pos = new Vector2(0.0f, (float)-Math.Sin(duration) * swipeSpeed);
                break;
            default:
                pos = new Vector2(0, 0);
                break;
        }
        rb2d.MovePosition(startPosition + pos);
        time += Time.fixedDeltaTime;
        if (durations[(int)state] <= time - stateStartTime)
        {
            RandomizePath();
            state = transitions[(int)state];
            stateStartTime = time;
            startPosition = rb2d.position;
        }
    }

    private void RandomizePath()
    {
        if (state == StateEnum.Down)
        {
            if (UnityEngine.Random.value <= 0.5f)
            {
                transitions[2] = StateEnum.SwipeRight;
                transitions[5] = StateEnum.ArcRight;
                transitions[1] = StateEnum.Up;
            }
            else
            {
                transitions[2] = StateEnum.ArcRight;
                transitions[1] = StateEnum.SwipeRight;
                transitions[5] = StateEnum.Up;
            }
        }
        if (state == StateEnum.Up)
        {
            if (UnityEngine.Random.value <= 0.5f)
            {
                transitions[3] = StateEnum.SwipeLeft;
                transitions[4] = StateEnum.ArcLeft;
                transitions[0] = StateEnum.Down;
            }
            else
            {
                transitions[3] = StateEnum.ArcLeft;
                transitions[0] = StateEnum.SwipeLeft;
                transitions[4] = StateEnum.Down;
            }
        }
    }

    private enum StateEnum
    {
        ArcLeft = 0,
        ArcRight,
        Down,
        Up,
        SwipeLeft,
        SwipeRight
    }
}
