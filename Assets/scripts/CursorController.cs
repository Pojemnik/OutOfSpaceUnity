using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public float yOffset;
    public float widthOffset;
    public AudioClip cursorClick;
    public float moveMultipler;

    private RectTransform rectTransform;
    private Image leftCursorImage;
    private Image rightCursorImage;
    private AudioSource source;
    private float startWidth;

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
        startWidth = buttonTransform.rect.width + widthOffset;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startWidth);
        source.PlayOneShot(cursorClick);
        StopAllCoroutines();
        StartCoroutine(MovingCoroutine());
    }

    public void OnPointerButtonExit(GameObject button)
    {
        leftCursorImage.color = Color.clear;
        rightCursorImage.color = Color.clear;
        StopAllCoroutines();
    }

    private IEnumerator MovingCoroutine()
    {
        float time = 0;
        while(true)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, startWidth + Mathf.Sin(time) / moveMultipler);
            time += 0.1f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
