using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICustomSlider : UnityEngine.UI.Slider
{
    public UnityEngine.Events.UnityEvent<bool> eventOnClick = null;

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log($"OnPointerDown => {eventData.ToString()}");
        eventOnClick?.Invoke(true);
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log($"OnPointerUp => {eventData.ToString()}");
        eventOnClick?.Invoke(false);
        base.OnPointerDown(eventData);
    }

}
