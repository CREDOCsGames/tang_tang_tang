using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image imageBackground;
    private Image imageController;
    Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x /= imageBackground.rectTransform.sizeDelta.x;
            touchPosition.y /= imageBackground.rectTransform.sizeDelta.y;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }
}
