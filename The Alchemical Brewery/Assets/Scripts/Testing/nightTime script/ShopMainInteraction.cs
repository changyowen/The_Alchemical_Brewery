using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMainInteraction : MonoBehaviour
{
    public enum ShopState
    {
        Ingredient,
        Upgrade
    }

    public GameObject[] changeStateButton_obj;
    public GameObject ingredientPanel;
    public GameObject upgradePanel;
    public GameObject fairyShopButton_obj;


    public ShopState currentShopState = ShopState.Ingredient;



    private void Update()
    {
        UpdateShopPanel();
    }

    void UpdateShopPanel()
    {
        switch(currentShopState)
        {
            case ShopState.Ingredient:
                {
                    //update panel active
                    ingredientPanel.SetActive(true);
                    upgradePanel.SetActive(false);
                    //update button active
                    UpdateStateButton(currentShopState);
                    break;
                }
            case ShopState.Upgrade:
                {
                    ingredientPanel.SetActive(false);
                    upgradePanel.SetActive(true);
                    UpdateStateButton(currentShopState);
                    break;
                }
        }
    }

    void UpdateStateButton(ShopState _shopState)
    {
        switch(_shopState)
        {
            case ShopState.Ingredient:
                {
                    changeStateButton_obj[0].transform.GetChild(1).gameObject.SetActive(false);
                    changeStateButton_obj[1].transform.GetChild(1).gameObject.SetActive(true);
                    break;
                }
            case ShopState.Upgrade:
                {
                    changeStateButton_obj[0].transform.GetChild(1).gameObject.SetActive(true);
                    changeStateButton_obj[1].transform.GetChild(1).gameObject.SetActive(false);
                    break;
                }
        }
    }

    public void ChangeShopState(int index)
    {
        switch(index)
        {
            case 0: //ingredient button
                {
                    currentShopState = ShopState.Ingredient;
                    break;
                }
            case 1: //upgrade button
                {
                    currentShopState = ShopState.Upgrade;
                    break;
                }
        }
    }

    public void CloseFairyShop()
    {
        fairyShopButton_obj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
