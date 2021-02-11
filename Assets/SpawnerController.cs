using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawningTemplate> levels;
    public UnityEvent allEniemiesDeadEvent;
    public List<GameObject> hpBarsPrefabs;

    private int enemiesAlive = 0;

    public void OnLevelStart(int levelNumber)
    {
        Vector2 maxBounds = new Vector2(6.35f, 4.5f);
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            GameObject enemy = Instantiate(enemyData.prefab, new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion());
            GameObject hpBar = Instantiate(hpBarsPrefabs[enemyData.health - 2]);
            hpBar.transform.parent = enemy.transform;
            hpBar.transform.localPosition = new Vector3(0, 0.3f, 0);
            hpBar.SetActive(true);
            switch (enemyData.aiType)
            {
                case AiType.SnakeRight:
                    {
                        SnakeAi ai = enemy.AddComponent<SnakeAi>();
                        ai.speed = 1.5f;
                        ai.moveSquence = new List<SnakeAi.DirectionBound>
                    {
                new SnakeAi.DirectionBound(new Vector2Int(1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false),
                new SnakeAi.DirectionBound(new Vector2Int(-1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false)
            };
                    }
                    break;
                case AiType.SnakeLeft:
                    {
                        SnakeAi ai = enemy.AddComponent<SnakeAi>();
                        ai.speed = 1.5f;
                        ai.moveSquence = new List<SnakeAi.DirectionBound>
                    {
                new SnakeAi.DirectionBound(new Vector2Int(-1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false),
                new SnakeAi.DirectionBound(new Vector2Int(1,0), maxBounds, true),
                new SnakeAi.DirectionBound(new Vector2Int(0,-1), new Vector2(0, -1), false)
            };
                    }
                    break;
                case AiType.Eight:
                    enemy.AddComponent<EightAi>().speed = 0.5f;
                    break;
                case AiType.Circle:
                    {
                        CircleAi ai = enemy.AddComponent<CircleAi>();
                        ai.speed = 1.0f;
                        ai.range = 1.8f;
                    }
                    break;
                default:
                    break;
            }
            enemy.SetActive(true);
            Health enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.deathEvent.AddListener(OnEnemyDeath);
            enemyHealth.startHealth = enemyData.health;
            enemiesAlive++;
        }
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive == 0)
        {
            allEniemiesDeadEvent.Invoke();
        }
    }

    public enum AiType
    {
        SnakeRight,
        SnakeLeft,
        Static,
        Eight,
        Circle
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
        public AiType aiType;

    }
}
