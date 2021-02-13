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
    private Vector2[] buttonsPositions =
    {
        new Vector2(0, 20),
        new Vector2(0, -45),
        new Vector2(0, -110)
    };

    void Awake()
    {
        textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        resumeButton = transform.Find("ResumeButton").gameObject;
        menuButton = transform.Find("MenuButton").gameObject;
    }

    public void OnLevelChange(int level)
    {
        PlaceButtons(-1, -1);
        textMesh.text = string.Format("Level {0}", level + 1);
        currentLevel = level;
        StartCoroutine(WaitCoroutine());
    }

    public void OnPauseStateChange(bool pause)
    {
        if (pause)
        {
            textMesh.text = string.Format("Game paused");
            PlaceButtons(1, 0);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPlayerDeath()
    {
        textMesh.text = string.Format("Game over");
        PlaceButtons(0, -1);
    }

    public void OnVictory()
    {
        textMesh.text = string.Format("You win");
        PlaceButtons(0, -1);
    }

    private void PlaceButtons(int menu, int resume)
    {
        if (menu == -1)
        {
            menuButton.SetActive(false);
        }
        else
        {
            menuButton.SetActive(true);
            menuButton.transform.localPosition = buttonsPositions[menu];
        }
        if (resume == -1)
        {
            resumeButton.SetActive(false);
        }
        else
        {
            resumeButton.SetActive(true);
            resumeButton.transform.localPosition = buttonsPositions[resume];
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        levelStart.Invoke(currentLevel);
        gameObject.SetActive(false);
    }
}
