using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManeuverAi2 : MonoBehaviour
{
    private float speed = 3;
    private float stateTime = 0.0f;
    private Vector2 lastPosition;
    private float lastRotation;
    private StateStruct nextState;
    private readonly float[] stateDurations = { 0, 0.6f, 1.2f, 1.2f };
    private Rigidbody2D rb2d;
    private MoveMode lastMode;
    private AutoShooter shooter;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shooter = GetComponent<AutoShooter>();
        nextState = GenerateNextState();
        lastMode = MoveMode.Regular;
        lastPosition = rb2d.position;
    }

    private void Update()
    {
        stateTime += Time.deltaTime;
        if (stateTime >= nextState.duration)
        {
            stateTime -= nextState.duration;
            lastPosition = nextState.finalPosition;
            lastRotation = nextState.targetRotation;
            nextState = GenerateNextState();
        }
        rb2d.position = Vector2.Lerp(lastPosition, nextState.finalPosition, stateTime / nextState.duration);
        rb2d.rotation = Mathf.LerpAngle(lastRotation, nextState.targetRotation, stateTime / nextState.duration);
    }

    private StateStruct GenerateNextState()
    {
        StateStruct newState = new StateStruct();
        MoveMode mode = MoveMode.Regular;
        if (Math.Abs(lastPosition.x) > 4.2f && lastMode == MoveMode.Regular)
        {
            mode = MoveMode.Rotate;
        }
        if (lastMode == MoveMode.Rotate)
        {
            mode = MoveMode.Dash;
        }
        if (lastMode == MoveMode.Dash)
        {
            mode = MoveMode.Unrotate;
        }
        switch (mode)
        {
            case MoveMode.Regular:
                newState.targetVariation = UnityEngine.Random.insideUnitCircle;
                newState.targetSector = nextState.targetSector;
                newState.targetRotation = 0;
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
                        newState.targetSector.y += UnityEngine.Random.Range(-1, 2);
                        break;
                    case 1:
                        newState.targetSector.y += UnityEngine.Random.Range(-1, 1);
                        break;
                    case -1:
                        newState.targetSector.y += UnityEngine.Random.Range(0, 2);
                        break;
                    default:
                        //This should never happen
                        newState.targetSector.y = 0;
                        break;
                }
                newState.finalPosition = SectorToPosition(newState.targetSector) + newState.targetVariation;
                shooter.enabled = true;
                break;
            case MoveMode.Dash:
                newState.targetVariation = Vector2.zero;
                newState.targetSector = nextState.targetSector;
                newState.targetSector.x = -newState.targetSector.x;
                newState.finalPosition = nextState.finalPosition;
                newState.finalPosition.x = -newState.finalPosition.x;
                newState.targetRotation = nextState.targetRotation;
                shooter.enabled = false;
                break;
            case MoveMode.Rotate:
                newState.targetVariation = Vector2.zero;
                newState.targetSector = nextState.targetSector;
                newState.finalPosition = nextState.finalPosition;
                if (newState.finalPosition.x > 0)
                {
                    newState.targetRotation = -90;
                }
                else
                {
                    newState.targetRotation = 90;
                }
                shooter.enabled = false;
                break;
            case MoveMode.Unrotate:
                newState.targetVariation = Vector2.zero;
                newState.targetSector = nextState.targetSector;
                newState.finalPosition = nextState.finalPosition;
                newState.targetRotation = 0;
                shooter.enabled = false;
                break;
            default:
                break;
        }
        if (mode == MoveMode.Regular)
        {
            newState.duration = Vector2.Distance(newState.finalPosition, nextState.finalPosition) / speed;
        }
        else
        {
            newState.duration = stateDurations[(int)mode];
        }
        lastMode = mode;
        return newState;
    }

    private Vector2 SectorToPosition(Vector2Int sector)
    {
        return new Vector2(sector.x * 4.0f, sector.y * 3);
    }

    private enum MoveMode
    {
        Regular = 0,
        Dash,
        Rotate,
        Unrotate
    }

    [System.Serializable]
    public struct StateStruct
    {
        public Vector2Int targetSector;
        public Vector2 targetVariation;
        public Vector2 finalPosition;
        public float targetRotation;
        public float duration;
    }
}
