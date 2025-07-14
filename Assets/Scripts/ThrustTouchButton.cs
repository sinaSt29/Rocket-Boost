using UnityEngine;
using UnityEngine.EventSystems;

public class ThrustTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Movement movement; // رفرنس به اسکریپت Movement

    private enum ButtonType
    {
        Thrust,
        Left,
        Right
    } // تعریف نوع دکمه

    [SerializeField] private ButtonType buttonType; // نوع دکمه

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Thrust:
                movement.isThrustPressed = true;
                break;
            case ButtonType.Left:
                movement.isRotatingLeft = true;
                break;
            case ButtonType.Right:
                movement.isRotatingRight = true;
                break;
        }
        // movement.isThrustPressed = true; // فعال کردن حرکت
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Thrust:
                movement.isThrustPressed = false;
                break;
            case ButtonType.Left:
                movement.isRotatingLeft = false;
                break;
            case ButtonType.Right:
                movement.isRotatingRight = false;
                break;
        }
    }
}