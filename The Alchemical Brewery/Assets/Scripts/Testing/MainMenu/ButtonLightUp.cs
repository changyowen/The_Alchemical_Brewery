using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLightUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;

    public Color disableColor;

    public void Start()
    {
        Canvas.ForceUpdateCanvases();
        Color colors = buttonImage.color;
        colors = disableColor;
        buttonImage.color = colors;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color colors = buttonImage.color;
        colors = Color.white;
        buttonImage.color = colors;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Color colors = buttonImage.color;
        colors = disableColor;
        buttonImage.color = colors;
    }
}
