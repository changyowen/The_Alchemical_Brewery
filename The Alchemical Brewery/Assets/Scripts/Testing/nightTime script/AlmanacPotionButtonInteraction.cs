using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlmanacPotionButtonInteraction : MonoBehaviour, IPointerClickHandler
{
    public int buttonIndex = 0;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        AlmanacPotionInformationHandler potionInfoHandler = transform.parent.parent.parent.parent.parent.GetComponent<AlmanacPotionInformationHandler>();

        if(potionInfoHandler != null)
        {
            potionInfoHandler.ChangeAlmanacPotionIndex(buttonIndex);
        }
    }
}
