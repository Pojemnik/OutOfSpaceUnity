using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawningTemplate> levels;
    public UnityEvent allEniemiesDeadEvent;

    private int enemiesAlive = 0;

    public void OnLevelStart(int levelNumber)
    {
        Vector2 maxBounds = new Vector2(6.35f, 4.5f);
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            GameObject enemy = Instantiate(enemyData.prefab, new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion());
            SnakeAi ai = enemy.AddComponent<SnakeAi>();
            ai.speed = 1.5f;
            ai.moveSquence = new List<SnakeAi.DirectionBound>
            {

                new SnakeAi.DirectionBound(new Vector2Int(1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false),
                new SnakeAi.DirectionBound(new Vector2Int(-1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false),
            };
            enemy.SetActive(true);
            Health enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.deathEvent.AddListener(OnEnemyDeath);
            enemiesAlive++;
        }
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
        if(enemiesAlive == 0)
        {
            allEniemiesDeadEvent.Invoke();
        }
    }
}

[System.Serializable]
public struct LevelSpawningTemplate
{
    public List<EnemySpawnData> enemies;
}

[System.Serializable]
public struct EnemySpawnData
{
    public Vector2 position;
    public GameObject prefab;
    public int health;
}
