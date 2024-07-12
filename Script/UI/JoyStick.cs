using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image imageBackground;
    private Image imageController;
    Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
    }

    private void Update()
    {
        Horizontal();
        Vertical();
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

    public void Horizontal()
    {
        PlayerMove.Instance.x = touchPosition.x;
    }

    public void Vertical()
    {
        PlayerMove.Instance.y = touchPosition.y;
    }
}
