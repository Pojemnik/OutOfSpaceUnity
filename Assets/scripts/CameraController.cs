using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;
    public float shakeForce;

    private Vector3 startPosition;
    private float timeLeft = 0;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public void ShakeCamera()
    {
        timeLeft = shakeTime;
    }

    private void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere * shakeForce;
            if(timeLeft <= 0)
            {
                transform.position = startPosition;
            }
        }
    }
}
