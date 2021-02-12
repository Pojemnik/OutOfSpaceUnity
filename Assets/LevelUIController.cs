using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUIController : MonoBehaviour
{
    public IntEvent levelStart;
    public float displayTime;

    private TMPro.TextMeshProUGUI textMesh;
    private GameObject resumeButton;
    private GameObject menuButton;
    private int currentLevel;

    void Awake()
    {
        textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        resumeButton = transform.Find("ResumeButton").gameObject;
        menuButton = transform.Find("MenuButton").gameObject;
    }

    public void OnLevelChange(int level)
    {
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        textMesh.text = string.Format("Level {0}", level + 1);
        currentLevel = level;
        StartCoroutine(WaitCoroutine());
    }

    public void OnPauseStateChange(bool pause)
    {
        if (pause)
        {
            textMesh.text = string.Format("Game paused");
            resumeButton.SetActive(true);
            menuButton.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        levelStart.Invoke(currentLevel);
        gameObject.SetActive(false);
    }
}
