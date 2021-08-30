using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChosenPotionMouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ChosenPotionInformation chosenPotionInformation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        chosenPotionInformation.mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chosenPotionInformation.mouseIn = false;
    }
}
