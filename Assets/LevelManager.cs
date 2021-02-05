using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public IntEvent levelChanged;
    void Start()
    {
        levelChanged.Invoke(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
