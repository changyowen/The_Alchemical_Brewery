using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePotionIconPanel : MonoBehaviour
{
    public PotionData potionData = null;
    public AlmanacPotionInformationHandler almanacScript = null;
    public ResultPanelHandler resultPanelScript = null;

    public void ChangePotionIcon(int buttonIndex)
    {
        if(potionData != null)
        {
            potionData.potionSpriteIndex = buttonIndex;
        }

        if(almanacScript != null)
        {
            almanacScript.UpdatePotionInformation();
            almanacScript.UpdateButtonData();
        }

        if(resultPanelScript != null)
        {
            resultPanelScript.RefreshPotionLogo();
        }

        Destroy(this.gameObject);
    }
}
