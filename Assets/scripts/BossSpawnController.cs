using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnController : MonoBehaviour
{
    public GameObject spawnerObject;
    public float jumpTime;
    public AiType enemyAiType;
    public Vector2 spawnOffset;

    private SpawnerController spawnerController;
    private EnemySpawnData spawnData;

    void Awake()
    {
        spawnerController = spawnerObject.GetComponent<SpawnerController>();
        spawnData = new EnemySpawnData
        {
            aiType = enemyAiType,
            health = 2,
            position = (Vector2)transform.position + spawnOffset,
            prefabID = 0
        };
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(jumpTime/2);
        spawnerController.SpawnEnemy(spawnData);
        yield return new WaitForSecondsRealtime(jumpTime/2);
        Destroy(gameObject);
    }
}
