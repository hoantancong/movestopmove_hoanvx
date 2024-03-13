using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform knob;
    [SerializeField] private RectTransform backround;
    private Vector2 inputVector;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backround, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = touchPosition.x / backround.sizeDelta.x;
            touchPosition.y = touchPosition.y / backround.sizeDelta.y;
            inputVector = new Vector2(touchPosition.x * 2, touchPosition.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            knob.anchoredPosition = new Vector2(inputVector.x * (backround.sizeDelta.x / 2), inputVector.y * (backround.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        knob.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    //get rect trans
    private void Awake()
    {
        if (knob == null)
        {
            knob = transform.GetChild(0).GetComponent<RectTransform>();
        }
        if (backround == null)
        {
            backround = GetComponent<RectTransform>();
        }
    }
    private void OnEnable()
    {
        //reset
        inputVector = Vector2.zero;
        //reset knob position
        knob.anchoredPosition = Vector2.zero;

    }

    public Vector2 GetInput()
    {
        return inputVector;
    }
}
