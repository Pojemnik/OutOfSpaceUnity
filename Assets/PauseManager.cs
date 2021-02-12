using UnityEngine.Events;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public BoolEvent pauseStateChanged;
    public GameObject UICanvas;

    private bool pausedByUser = false;
    private bool systemPause = false;
    private bool lostFocus = false;
    private float pauseCooldown = 0.5f;
    private float time = 0;
    private bool canPause = true;

    private void OnApplicationPause(bool pause)
    {
        systemPause = pause;
        if(pause)
        {
            pausedByUser = true;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        lostFocus = !focus;
        if(!focus)
        {
            pausedByUser = true;
        }
    }

    public void OnLevelChange()
    {
        canPause = false;
    }

    public void OnLevelStart()
    {
        canPause = true;
    }

    public void OnResumeButtonPressed()
    {
        time = pauseCooldown;
        pausedByUser = false;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.unscaledDeltaTime;
        }
        if (canPause)
        {
            if (Input.GetButton("Cancel") && time <= 0)
            {
                time = pauseCooldown;
                pausedByUser = !pausedByUser;
            }
            if (pausedByUser || systemPause || lostFocus)
            {
                Time.timeScale = 0;
                UICanvas.SetActive(true);
                pauseStateChanged.Invoke(true);
            }
            else
            {
                Time.timeScale = 1;
                UICanvas.SetActive(true);
                pauseStateChanged.Invoke(false);
            }
        }
    }
}

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
