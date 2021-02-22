using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public int startLevel;
    public UnityEvent startJump;
    public UnityEvent victory;
    public IntEvent changeLevel;
    public GameObject player;
    public GameObject UICanvas;

    private int currentLevel;
    void Awake()
    {
        string[] arguments = System.Environment.GetCommandLineArgs();
        if(arguments.Length == 2)
        {
            try
            {
                startLevel = int.Parse(arguments[1]);
            }
            catch (System.FormatException e)
            {
                print(e);
            }
        }
        currentLevel = startLevel;
        UICanvas.SetActive(true);
        changeLevel.Invoke(currentLevel);
    }

    public void OnAllEnemiesDead()
    {
        currentLevel++;
        if (currentLevel < levelNumber)
        {
            startJump.Invoke();
        }
        else
        {
            player.SetActive(false);
            UICanvas.SetActive(true);
            victory.Invoke();
        }
    }

    public void OnPlayerDeath()
    {
        UICanvas.SetActive(true);
        UICanvas.GetComponent<LevelUIController>().OnPlayerDeath();
    }

    public void OnJumpEnd()
    {
        player.SetActive(true);
        UICanvas.SetActive(true);
        changeLevel.Invoke(currentLevel);
    }

    public void OnRestartButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");
    }

    public void OnMenuButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
