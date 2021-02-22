using UnityEngine.Events;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public BoolEvent pauseStateChanged;
    public GameObject UICanvas;
    public GameObject settingsCanvas;

    private bool pausedByUser = false;
    private bool systemPause = false;
    private bool lostFocus = false;
    private float pauseCooldown = 0.5f;
    private float time = 0;
    private bool canPause = true;
    private bool settingsEnabled = false;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnApplicationPause(bool pause)
    {
        systemPause = pause;
        if (pause)
        {
            pausedByUser = true;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        lostFocus = !focus;
        if (!focus)
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

    public void OnVictory()
    {
        canPause = false;
    }

    public void OnPlayerDeath()
    {
        canPause = false;
    }

    public void OnSettingsButtonPressed()
    {
        UICanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        settingsEnabled = true;
    }

    public void OnBackButtonPressed()
    {
        settingsCanvas.SetActive(false);
        UICanvas.SetActive(true);
        settingsEnabled = false;
        PlayerPrefs.Save();
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
            bool pause = pausedByUser || systemPause || lostFocus;
            if (pause && !settingsEnabled)
            {
                Pause();
            }
            if (!pause && Time.timeScale == 0 && !settingsEnabled)
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        UICanvas.SetActive(true);
        pauseStateChanged.Invoke(true);
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        UICanvas.SetActive(true);
        pauseStateChanged.Invoke(false);
    }
}

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
