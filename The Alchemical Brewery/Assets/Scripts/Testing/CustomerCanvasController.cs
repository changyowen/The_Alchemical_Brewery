using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCanvasController : MonoBehaviour
{
    public CustomerInformationHandler customerInformationHandler;
    public Text testingText;

    void Update()
    {
        testingText.text = "" + customerInformationHandler.customerClass.preferPotion;
    }
}
