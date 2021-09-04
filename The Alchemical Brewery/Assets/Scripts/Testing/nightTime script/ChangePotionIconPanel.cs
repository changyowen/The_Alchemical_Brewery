using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePotionIconPanel : MonoBehaviour
{
    public PotionData potionData = null;

    public void ChangePotionIcon(int buttonIndex)
    {
        if(potionData != null)
        {
            potionData.potionSpriteIndex = buttonIndex;
        }

        Destroy(this.gameObject);
    }
}
