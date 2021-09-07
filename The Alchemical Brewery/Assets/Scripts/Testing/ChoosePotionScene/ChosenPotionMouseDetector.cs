using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChosenPotionMouseDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ChosenPotionInformation chosenPotionInformation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ChosenPotionHandler.Instance != null)
        {
            if(!ChosenPotionHandler.Instance.choosingIngredientMode)
            {
                chosenPotionInformation.mouseIn = true;
            }
            else
            {
                chosenPotionInformation.mouseIn = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        chosenPotionInformation.mouseIn = false;
    }
}
