using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public IntEvent levelChanged;

    private int currentLevel;
    void Start()
    {
        currentLevel = 0;
        levelChanged.Invoke(currentLevel);
    }

    public void OnAllEnemiesDead()
    {
        currentLevel++;
        if (currentLevel <= levelNumber)
        {
            levelChanged.Invoke(currentLevel);
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
}

[System.Serializable]
public class IntEvent : UnityEvent<int> { }
