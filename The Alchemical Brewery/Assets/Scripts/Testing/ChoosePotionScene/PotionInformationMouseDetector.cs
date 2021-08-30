using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionInformationMouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public PotionInformationHandler potionInformationHandler;

    public void OnPointerEnter(PointerEventData eventData)
    {
        potionInformationHandler.mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        potionInformationHandler.mouseIn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        potionInformationHandler.ChoosePotion();
    }
}
