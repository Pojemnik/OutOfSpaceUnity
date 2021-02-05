using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawningTemplate> levels;

    private List<GameObject> enemiesAlive;
    void Start()
    {
        enemiesAlive = new List<GameObject>();
    }

    public void OnLevelChange(int levelNumber)
    {
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            enemiesAlive.Add(Instantiate(enemyData.prefab, new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion()));
            var component = enemiesAlive[0].AddComponent<SnakeAi>();
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
