using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public float yOffset;
    public float widthOffset;
    public AudioClip cursorClick;

    private RectTransform rectTransform;
    private Image leftCursorImage;
    private Image rightCursorImage;
    private AudioSource source;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach (RectTransform rect in children)
        {
            if (rect.name == "CursorLeft")
            {
                leftCursorImage = rect.gameObject.GetComponent<Image>();
            }
            if (rect.name == "CursorRight")
            {
                rightCursorImage = rect.gameObject.GetComponent<Image>();
            }
        }
        source = GetComponent<AudioSource>();
    }

    public void OnPointerButtonEnter(GameObject button)
    {
        leftCursorImage.color = Color.white;
        rightCursorImage.color = Color.white;
        RectTransform buttonTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = buttonTransform.anchoredPosition + new Vector2(0, yOffset);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonTransform.rect.width + widthOffset);
        source.PlayOneShot(cursorClick);
    }

    public void OnPointerButtonExit(GameObject button)
    {
        leftCursorImage.color = Color.clear;
        rightCursorImage.color = Color.clear;
    }
}
