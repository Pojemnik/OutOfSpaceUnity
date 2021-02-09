using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUIController : MonoBehaviour
{
    public IntEvent levelStart;
    public float displayTime;

    private TMPro.TextMeshProUGUI textMesh;
    private int currentLevel;

    void Awake()
    {
        textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void OnLevelChange(int level)
    {
        textMesh.text = string.Format("Level {0}", level+1);
        currentLevel = level;
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSecondsRealtime(displayTime);
        levelStart.Invoke(currentLevel);
        gameObject.SetActive(false);
    }
}
