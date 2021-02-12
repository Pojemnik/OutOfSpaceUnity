﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public int startLevel;
    public UnityEvent startJump;
    public IntEvent changeLevel;
    public GameObject player;
    public GameObject UICanvas;

    private int currentLevel;
    void Start()
    {
        currentLevel = startLevel;
        UICanvas.SetActive(true);
        changeLevel.Invoke(currentLevel);
    }

    public void OnAllEnemiesDead()
    {
        currentLevel++;
        if (currentLevel <= levelNumber)
        {
            startJump.Invoke();
        }
        else
        {
            print("You win");
            //end the game with you won screen
        }
    }

    public void OnPlayerDeath()
    {
        print("Game over");
        //End the game with game over screen
    }

    public void OnJumpEnd()
    {
        player.SetActive(true);
        UICanvas.SetActive(true);
        changeLevel.Invoke(currentLevel);
    }

    public void OnMenuButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
    }
}

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
