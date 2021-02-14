using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawnData> levels;
    public UnityEvent allEniemiesDeadEvent;
    public List<GameObject> hpBarsPrefabs;
    public List<GameObject> enemiesPrefabs;

    private int enemiesAlive = 0;

    public void OnLevelStart(int levelNumber)
    {
        Vector2 maxBounds = new Vector2(6.35f, 4.5f);
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            GameObject enemy = Instantiate(enemiesPrefabs[enemyData.prefabID], new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion());
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
                case AiType.CircleLeft:
                    {
                        CircleAi ai = enemy.AddComponent<CircleAi>();
                        ai.speed = 1.0f;
                        ai.range = 1.8f;
                    }
                    break;
                case AiType.CircleRight:
                    {
                        CircleAi ai = enemy.AddComponent<CircleAi>();
                        ai.speed = 1.0f;
                        ai.range = -1.8f;
                    }
                    break;
                case AiType.DiagonalLeft:
                    {
                        DiagonalAi ai = enemy.AddComponent<DiagonalAi>();
                        ai.speed = 1.3f;
                        ai.range = 1.2f;
                        ai.direction = 0.5f;
                    }
                    break;
                case AiType.DiagonalRight:
                    {
                        DiagonalAi ai = enemy.AddComponent<DiagonalAi>();
                        ai.speed = 1.3f;
                        ai.range = 1.2f;
                        ai.direction = -0.5f;
                    }
                    break;
                case AiType.Maneuver1:
                    {
                        ManeuverAi1 ai = enemy.AddComponent<ManeuverAi1>();
                        ai.arcSpeed = new Vector2(5.0f, 2.0f);
                        ai.swipeSpeed = 6.0f;
                        ai.upDownSpeed = 1.0f;
                        ai.startDirection = 1;
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
}
