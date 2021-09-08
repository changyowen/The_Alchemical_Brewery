using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrinkingPotionInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int holderIndex = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 70f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(PocketSystem.Instance.DrinkPotion(holderIndex));
    }
}
