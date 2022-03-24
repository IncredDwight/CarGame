using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BoxSlider : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform _clickRect;
    [SerializeField] private RectTransform _handleRect;
    
    private Vector2 _value;

    public UnityEvent OnValueChanged;

    private void Update()
    {
        
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_clickRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            return;

        localPoint.x = Mathf.Clamp(localPoint.x, 0, _clickRect.rect.width);
        localPoint.y = Mathf.Clamp(localPoint.y, 0, _clickRect.rect.height);
        _handleRect.localPosition = localPoint;

        _value.x = localPoint.x / _clickRect.rect.width;
        _value.y = localPoint.y / _clickRect.rect.height;

        OnValueChanged.Invoke();
    }

    public Vector2 GetValue()
    {
        return _value;
    }

    public void SetValue(float x, float y)
    {
        _value.x = x;
        _value.y = y;
        _handleRect.localPosition = new Vector2(x * _clickRect.rect.width, y * _clickRect.rect.height);
        OnValueChanged.Invoke();
    }
}
