using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawningTemplate> levels;
    public UnityEvent allEniemiesDeadEvent;

    private int enemiesAlive = 0;

    public void OnLevelChange(int levelNumber)
    {
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            GameObject enemy = Instantiate(enemyData.prefab, new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion());
            var component = enemy.AddComponent<SnakeAi>();
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
