using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarController : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer rendererComponent;
    private int currentHealth;

    void Start()
    {
        rendererComponent = GetComponent<SpriteRenderer>();
        currentHealth = sprites.Length;
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
        currentHealth = newHealth;
    }

    public void Healed()
    {
        currentHealth++;
        if (currentHealth > sprites.Length)
        {
            currentHealth = sprites.Length;
        }
        else
        {
            rendererComponent.sprite = sprites[currentHealth - 1];
        }
    }
}
