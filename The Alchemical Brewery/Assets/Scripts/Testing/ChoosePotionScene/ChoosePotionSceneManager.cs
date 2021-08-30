using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePotionSceneManager : MonoBehaviour
{
    private void Start()
    {
        SaveManager.Load();
        Canvas.ForceUpdateCanvases();
        ChoosingPotionHandler.Instance.AssignAllAvailablePotion();
        ChoosingPotionHandler.Instance.InstantiateAvailablePotion();
        ChosenPotionHandler.Instance.UpdateChosenPotion();
    }

}
