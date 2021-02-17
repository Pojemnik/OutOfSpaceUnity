using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer rendererComponent;

    void Start()
    {
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    public void Damaged(int newHealth)
    {
        if (newHealth > sprites.Length)
        {
            print("Incorrect newHealth value in HpBarController");
            throw new System.IndexOutOfRangeException();
        }
        else if (newHealth > 0)
        {
            rendererComponent.sprite = sprites[newHealth - 1];
        }
    }
}
