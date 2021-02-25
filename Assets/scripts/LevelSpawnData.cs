using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelSpawnData", order = 1)]
public class LevelSpawnData : ScriptableObject
{
    public List<EnemySpawnData> enemies;
}

public enum AiType
{
    SnakeRight,
    SnakeLeft,
    Static,
    Eight,
    CircleLeft,
    CircleRight,
    DiagonalLeft,
    DiagonalRight,
    Maneuver1,
    Maneuver2,
    Boss
}

[System.Serializable]
public struct EnemySpawnData
{
    public Vector2 position;
    public int prefabID;
    public int health;
    public AiType aiType;

}
