using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color rolloverColor;
    public Color baseColor;
    public Color selectedColor;
    public TextMeshProUGUI text;
    public bool isActive = true;

    void Start()
    {
        text.color = baseColor;
    }
    //void OnMouseOver()
    //{
    //    text.color = rolloverColor;
    //}

    //void OnMouseExit()
    //{
    //    text.color = baseColor;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isActive)
            text.color = rolloverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isActive)
            text.color = baseColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isActive)
            text.color = baseColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // maybe
    }

}
