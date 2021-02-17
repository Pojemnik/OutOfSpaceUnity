using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cursor;

    private CursorController cursorController;

    private void Awake()
    {
        cursorController = cursor.GetComponent<CursorController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorController.OnPointerButtonEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorController.OnPointerButtonExit(gameObject);
    }
}
