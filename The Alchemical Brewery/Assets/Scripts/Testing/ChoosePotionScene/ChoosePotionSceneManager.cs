using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePotionSceneManager : MonoBehaviour
{
    public static ChoosePotionSceneManager Instance { get; private set; }

    [System.NonSerialized] public List<CustomerData> customerTypeToday = null;

    public bool testing = false;

    public void Awake()
    {
        Instance = this;

        if(testing)
        {
            SaveManager.Load();
        }
    }

    private void Start()
    {
        ///CHOSING & CHOSEN POTION PANEL
        Canvas.ForceUpdateCanvases();
        ChoosingPotionHandler.Instance.AssignAllAvailablePotion();
        ChoosingPotionHandler.Instance.InstantiateAvailablePotion();
        ChosenPotionHandler.Instance.UpdateChosenPotion();

        ///GET CUSTOMER TYPE TODAY (IF NOT NULL)
        if(StageManager.customerTypeToday != null)
        {
            customerTypeToday = StageManager.customerTypeToday;
        }

        ///START SPAWNING CUSTOMER
        StartCoroutine(CustomerEntranceHandler.Instance.SpawningCustomer());
    }

    public void OpenShop()
    {
        //
    }
    

}
