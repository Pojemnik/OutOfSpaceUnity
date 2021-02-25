using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerController : MonoBehaviour
{
    public List<LevelSpawnData> levels;
    public UnityEvent levelFinshedEvent;
    public List<GameObject> hpBarsPrefabs;
    public List<GameObject> enemiesPrefabs;
    public Vector2 maxBounds;
    public GameObject bossSpawnPrefab;
    public GameObject bossPortalPrefab;

    private int enemiesAlive = 0;
    private List<Health> bossSpawnedEnemiesHealths;
    private int currentLevel;
    private int hpToPickUp = 0;

    private void Awake()
    {
        bossSpawnedEnemiesHealths = new List<Health>();    
    }

    public void OnLevelStart(int levelNumber)
    {
        currentLevel = levelNumber;
        foreach (EnemySpawnData enemyData in levels[levelNumber].enemies)
        {
            SpawnEnemy(enemyData);
        }
    }

    private Health SpawnEnemyLocal(EnemySpawnData enemyData)
    {
        GameObject enemy = Instantiate(enemiesPrefabs[enemyData.prefabID], new Vector3(enemyData.position.x, enemyData.position.y, 0), new Quaternion());
        Health enemyHealth = enemy.GetComponent<Health>();
        enemyHealth.deathEvent.AddListener(OnEnemyDeath);
        enemyHealth.startHealth = enemyData.health;
        GameObject hpBar;
        if (enemyData.health <= 5)
        {
            hpBar = Instantiate(hpBarsPrefabs[enemyData.health - 2]);
        }
        else
        {
            hpBar = Instantiate(hpBarsPrefabs[4]);
        }
        hpBar.transform.parent = enemy.transform;
        hpBar.transform.localPosition = new Vector2(0, 0.3f) + enemyHealth.hpBarOffset;
        hpBar.SetActive(true);
        switch (enemyData.aiType)
        {
            case AiType.SnakeRight:
                {
                    SnakeAi ai = enemy.AddComponent<SnakeAi>();
                    ai.speed = 1.5f;
                    ai.downBound = -1;
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
                    ai.downBound = -1;
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
            case AiType.Maneuver2:
                {
                    ManeuverAi2 ai = enemy.AddComponent<ManeuverAi2>();
                }
                break;
            case AiType.Boss:
                {
                    BossAi ai = enemy.AddComponent<BossAi>();
                    ai.startAttack = BossAi.BossAttack.Maneuver;
                    ai.spawnPrefab = bossSpawnPrefab;
                    ai.portalPrefab = bossPortalPrefab;
                    enemyHealth.deathEvent.AddListener(OnBossDeath);
                }
                break;
            default:
                break;
        }
        enemy.SetActive(true);
        enemiesAlive++;
        if(enemy.GetComponent<HpDropper>() != null)
        {
            hpToPickUp++;
        }
        return enemyHealth;
    }

    public void SpawnEnemy(EnemySpawnData enemyData)
    {
        Health enemyHealth = SpawnEnemyLocal(enemyData);
        if (currentLevel == levels.Count - 1 && enemyData.prefabID != 5)
        {
            bossSpawnedEnemiesHealths.Add(enemyHealth);
        }
    }

    public void OnBossDeath()
    {
        foreach(Health enemyHealth in bossSpawnedEnemiesHealths)
        {
            if(enemyHealth != null)
            {
                enemyHealth.Die();
            }
        }
    }

    public void OnPickedUpHp()
    {
        hpToPickUp--;
        if (enemiesAlive == 0 && hpToPickUp == 0)
        {
            levelFinshedEvent.Invoke();
        }
    }

    public void OnEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive == 0 && hpToPickUp == 0)
        {
            levelFinshedEvent.Invoke();
        }
    }
}
