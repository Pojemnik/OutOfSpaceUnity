using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalController : MonoBehaviour
{
    public UnityEvent jumpEndEvent;
    public float jumpTime;

    void Awake()
    {
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(jumpTime);
        jumpEndEvent.Invoke();
        Destroy(gameObject);
    }
}
