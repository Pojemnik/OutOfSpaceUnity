using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUIController : MonoBehaviour
{
    public IntEvent levelStart;
    public float displayTime;
    public GameObject cursor;

    private TMPro.TextMeshProUGUI textMesh;
    private GameObject resumeButton;
    private GameObject menuButton;
    private GameObject restartButton;
    private GameObject settingsButton;
    private int currentLevel;
    private CursorController cursorController;
    private Vector2[] buttonsPositions =
    {
        new Vector2(0, 65),
        new Vector2(0, -5),
        new Vector2(0, -75),
        new Vector2(0, -145)
    };

    void Awake()
    {
        textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        resumeButton = transform.Find("ResumeButton").gameObject;
        menuButton = transform.Find("MenuButton").gameObject;
        restartButton = transform.Find("RestartButton").gameObject;
        settingsButton = transform.Find("SettingsButton").gameObject;
        cursorController = cursor.GetComponent<CursorController>();
    }

    public void OnLevelChange(int level)
    {
        cursorController.OnPointerButtonExit(gameObject);
        PlaceButtons(-1, -1, -1, -1);
        textMesh.text = string.Format("Level {0}", level + 1);
        currentLevel = level;
        StartCoroutine(WaitCoroutine());
    }

    public void OnPauseStateChange(bool pause)
    {
        if (pause)
        {
            textMesh.text = string.Format("Game paused");
            PlaceButtons(2, 0, 3, 1);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPlayerDeath()
    {
        textMesh.text = string.Format("Game over");
        PlaceButtons(0, -1, 1, -1);
    }

    public void OnVictory()
    {
        textMesh.text = string.Format("You win");
        PlaceButtons(0, -1, -1, -1);
    }


    private void PlaceButtons(int menu, int resume, int restart, int settings)
    {
        PlaceButton(menuButton, menu);
        PlaceButton(resumeButton, resume);
        PlaceButton(restartButton, restart);
        PlaceButton(settingsButton, settings);
    }

    private void PlaceButton(GameObject button, int position)
    {
        if (position == -1)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
            button.transform.localPosition = buttonsPositions[position];
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(displayTime);
        levelStart.Invoke(currentLevel);
        gameObject.SetActive(false);
    }
}
