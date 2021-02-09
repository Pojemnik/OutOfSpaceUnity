using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalController : MonoBehaviour
{
    public UnityEvent jumpEndEvent;
    public void OnJumpEnd()
    {
        jumpEndEvent.Invoke();
        Destroy(gameObject);
    }
}
