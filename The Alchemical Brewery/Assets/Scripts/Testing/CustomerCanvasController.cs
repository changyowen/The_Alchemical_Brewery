using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCanvasController : MonoBehaviour
{
    public CustomerInformationHandler customerInformationHandler;
    public ScriptableObjectHolder SO_holder;

    public Text testingText;
    public Image IconImage;

    void Update()
    {
        PotionData potionData = StageManager.potionListToday[customerInformationHandler.customerClass.preferPotion];
        IconImage.sprite = SO_holder.potionIconList[potionData.potionSpriteIndex];
        testingText.text = "" + customerInformationHandler.customerClass.preferPotion;
    }
}
