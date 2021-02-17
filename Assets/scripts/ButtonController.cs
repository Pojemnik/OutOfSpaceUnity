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
        Debug.Log("Pointer Enter");
        cursorController.OnPointerButtonEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        cursorController.OnPointerButtonExit(gameObject);
    }
}
