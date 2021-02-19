using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : ManeuverAi2
{
    public BossAttack startAttack;
    public GameObject spawnPrefab;
    public GameObject portalPrefab;

    private BossAttack currentAttack;
    private float attackTime;
    private float maneuverDuration = 4;
    private int attackIterator = 0;
    private const float defaultDuration = 2;

    private readonly StateStruct[] swipeAttack =
    {
        new StateStruct
        {
            finalPosition = new Vector2(-5, 4),
            duration = 2
        },
        new StateStruct
        {
            finalPosition = new Vector2(5, 4),
            duration = 1
        }
    };

    private readonly StateStruct defaultState = new StateStruct
    {
        finalPosition = new Vector2(0, 4),
        duration = defaultDuration
    };

    protected override void Awake()
    {
        currentAttack = startAttack;
        base.Awake();
    }

    protected override void Update()
    {
        if (currentAttack == BossAttack.Maneuver)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= maneuverDuration)
            {
                currentAttack = GenerateNextAttack();
                InitializeNextAttack();
            }
        }
        stateTime += Time.deltaTime;
        if (stateTime >= nextState.duration)
        {
            stateTime -= nextState.duration;
            lastPosition = nextState.finalPosition;
            switch (currentAttack)
            {
                case BossAttack.Maneuver:
                    nextState = GenerateNextState();
                    break;
                case BossAttack.Swipe:
                    if (attackIterator < swipeAttack.Length - 1)
                    {
                        attackIterator++;
                        nextState = swipeAttack[attackIterator];
                        shooter.shootCooldown = 0.1f;
                    }
                    else
                    {
                        currentAttack = GenerateNextAttack();
                        InitializeNextAttack();
                    }
                    break;
                case BossAttack.Spawn:
                    if (attackIterator < 1)
                    {
                        attackIterator++;
                        nextState = defaultState;
                    }
                    else
                    {
                        GameObject spawn = Instantiate(spawnPrefab, new Vector2(-4, 4), Quaternion.identity);
                        spawn.GetComponent<BossSpawnController>().enemyAiType = AiType.SnakeLeft;
                        spawn.SetActive(true);
                        spawn = Instantiate(spawnPrefab, new Vector2(4, 4), Quaternion.identity);
                        spawn.GetComponent<BossSpawnController>().enemyAiType = AiType.SnakeRight;
                        spawn.SetActive(true);
                        currentAttack = GenerateNextAttack();
                        InitializeNextAttack();
                    }
                    break;
                case BossAttack.Portal:
                    if (attackIterator < 1)
                    {
                        attackIterator++;
                        nextState = defaultState;
                    }
                    else
                    {
                        GameObject portal1 = Instantiate(portalPrefab, new Vector2(-6.5f, 4), Quaternion.Euler(0, 0, 90));
                        portal1.GetComponent<BossPortalController>().projectileSpeed = 5;
                        portal1.SetActive(true);
                        GameObject portal2 = Instantiate(portalPrefab, new Vector2(6.5f, 4), Quaternion.Euler(0, 0, 270));
                        portal2.GetComponent<BossPortalController>().projectileSpeed = -5;
                        portal2.SetActive(true);
                        currentAttack = GenerateNextAttack();
                        InitializeNextAttack();
                    }
                    break;
            }
        }
        rb2d.position = Vector2.Lerp(lastPosition, nextState.finalPosition, stateTime / nextState.duration);
    }

    protected override StateStruct GenerateNextState()
    {
        StateStruct newState = new StateStruct
        {
            targetVariation = UnityEngine.Random.insideUnitCircle,
            targetSector = nextState.targetSector,
            targetRotation = 0
        };
        switch (newState.targetSector.x)
        {
            case 0:
                newState.targetSector.x += UnityEngine.Random.Range(-1, 2);
                break;
            case -1:
                newState.targetSector.x += UnityEngine.Random.Range(0, 2);
                break;
            case 1:
                newState.targetSector.x += UnityEngine.Random.Range(-1, 1);
                break;
            default:
                //This should never happen
                newState.targetSector.x = 0;
                break;
        }
        switch (newState.targetSector.y)
        {
            case 0:
                newState.targetSector.y += UnityEngine.Random.Range(0, 2);
                break;
            case 1:
                newState.targetSector.y += UnityEngine.Random.Range(-1, 1);
                break;
            default:
                //This should never happen
                newState.targetSector.y = 0;
                break;
        }
        newState.finalPosition = SectorToPosition(newState.targetSector) + newState.targetVariation;
        shooter.enabled = true;
        newState.duration = Vector2.Distance(newState.finalPosition, nextState.finalPosition) / speed;
        return newState;
    }

    private BossAttack GenerateNextAttack()
    {
        stateTime = 0;
        int attackNumber = Random.Range(0, 3);
        if (attackNumber >= (int)currentAttack)
        {
            attackNumber++;
        }
        BossAttack attack = (BossAttack)attackNumber;
        //print(attack);
        return attack;
    }

    private void InitializeNextAttack()
    {
        lastPosition = rb2d.position;
        switch (currentAttack)
        {
            case BossAttack.Maneuver:
                attackTime = 0;
                nextState = GenerateNextState();
                shooter.shootCooldown = 0.9f;
                break;
            case BossAttack.Swipe:
                nextState = swipeAttack[0];
                nextState.duration = CalculateDuration(lastPosition, nextState.finalPosition);
                attackIterator = 0;
                shooter.shootCooldown = 0.9f;
                break;
            case BossAttack.Spawn:
                nextState = defaultState;
                nextState.duration = CalculateDuration(lastPosition, nextState.finalPosition);
                attackIterator = 0;
                shooter.shootCooldown = 0.9f;
                break;
            case BossAttack.Portal:
                nextState = defaultState;
                nextState.duration = CalculateDuration(lastPosition, nextState.finalPosition);
                attackIterator = 0;
                shooter.shootCooldown = 0.9f;
                break;
            default:
                nextState = defaultState;
                break;
        }
    }

    private float CalculateDuration(Vector2 a, Vector2 b)
    {
        float dist = Vector2.Distance(a, b);
        if(dist != 0)
        {
            return dist / speed;
        }
        return defaultDuration;
    }

    public enum BossAttack
    {
        Maneuver,
        Swipe,
        Spawn,
        Portal
    }
}
