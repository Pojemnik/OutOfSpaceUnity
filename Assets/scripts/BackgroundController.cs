using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public List<Sprite> backgrounds;

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgrounds[0];
    }

    public void OnLevelChange(int newLevel)
    {
        spriteRenderer.sprite = backgrounds[newLevel];
    }
}
