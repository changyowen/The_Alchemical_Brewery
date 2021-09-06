using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLightUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color disableColor;

    public void Start()
    {
        Canvas.ForceUpdateCanvases();
        Color colors = transform.GetChild(0).GetComponent<Image>().color;
        colors = disableColor;
        transform.GetChild(0).GetComponent<Image>().color = colors;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("YESS");
        Color colors = transform.GetChild(0).GetComponent<Image>().color;
        colors = Color.white;
        transform.GetChild(0).GetComponent<Image>().color = colors;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Color colors = transform.GetChild(0).GetComponent<Image>().color;
        colors = disableColor;
        transform.GetChild(0).GetComponent<Image>().color = colors;
    }
}
