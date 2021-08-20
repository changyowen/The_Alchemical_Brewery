using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public static class PlayerProfile 
{
    public static string profileName = "NewPlayer";
    public static List<PotionData> acquiredPotion = new List<PotionData>();
    public static IngredientProfile[] ingredientProfile = new IngredientProfile[20];
    public static CustomerProfile[] customerProfile = new CustomerProfile[10];
    public static FairyShopProfile fairyShopProfile = new FairyShopProfile();
    public static ShopProfile shopProfile = new ShopProfile();

    public static void LoadData(SaveData saveData)
    {
        profileName = saveData.profileName;
        acquiredPotion = saveData.acquiredPotion;
        customerProfile = saveData.customerProfile;
    }

    public static void NewGameData(string _profileName)
    {
        ///ASSIGN PROFILE NAME
        profileName = _profileName;

        ///ASSIGN INGREDIENT PROFILE CLASS
        for (int i = 0; i < ingredientProfile.Length; i++)
        {
            IngredientProfile newIngredientProfile = new IngredientProfile();
            ingredientProfile[i] = newIngredientProfile;
            //assign index
            ingredientProfile[i].ingredientIndex = i + 1;
        }

        ///ASSIGN CUSTOMER PROFILE CLASS
        for (int i = 0; i < customerProfile.Length; i++)
        {
            CustomerProfile newCustomerProfile = new CustomerProfile();
            customerProfile[i] = newCustomerProfile;
        }

        ///UNLOCK STUFF FOR START GAME
        //unlock first customer
        for (int i = 0; i < 1; i++)
        {
            customerProfile[i].UnlockThisCustomer();
        }
      
        //unlock first 4 ingredients
        for (int i = 0; i < 4; i++)
        {
            ingredientProfile[i].UnlockThisIngredient();
        }
    }

    public static void GM_TestingUse()
    {
        profileName = "SuperLegzai";

        ///ASSIGN INGREDIENT PROFILE CLASS
        for (int i = 0; i < ingredientProfile.Length; i++)
        {
            IngredientProfile newIngredientProfile = new IngredientProfile();
            ingredientProfile[i] = newIngredientProfile;
            //assign index
            ingredientProfile[i].ingredientIndex = i + 1;
        }

        ///ASSIGN CUSTOMER PROFILE CLASS
        for (int i = 0; i < customerProfile.Length; i++)
        {
            CustomerProfile newCustomerProfile = new CustomerProfile();
            customerProfile[i] = newCustomerProfile;
        }

        ///UNLOCK STUFF FOR START GAME
        //unlock first customer
        for (int i = 0; i < 7; i++)
        {
            customerProfile[i].UnlockThisCustomer();
        }
        //unlock first 4 ingredients
        for (int i = 0; i < 20; i++)
        {
            ingredientProfile[i].UnlockThisIngredient();
        }
        //unlock all stuff in fairy shop
        fairyShopProfile.UnlockAll();
        //purchase all stuff for shop
        shopProfile.PurchaseAll();
    }
}

[Serializable]
public class ShopProfile
{
    [Range(1, 3)] public int counterPurchased = 1;
    [Range(1, 10)] public int magicChestPurchased = 4;
    [Range(1, 3)] public int potPurchased = 1;
    public bool[] refinementStationPurchased = new bool[2] { false, false };

    public bool increaseCustomerAppearRate = false;

    public void PurchaseAll()
    {
        counterPurchased = 3;
        magicChestPurchased = 10;
        potPurchased = 3;
        increaseCustomerAppearRate = true;
        for (int i = 0; i < refinementStationPurchased.Length; i++)
        {
            refinementStationPurchased[i] = true;
        }
    }
}

[Serializable]
public class FairyShopProfile
{
    [Range(1, 3)] public int counterUnlocked = 1;
    [Range(1, 10)] public int magicChestUnlocked = 5;
    [Range(1, 3)] public int potUnlocked = 1;
    public bool[] refinementStationUnlocked = new bool[2] { false, false };

    public void UnlockAll()
    {
        counterUnlocked = 3;
        magicChestUnlocked = 10;
        potUnlocked = 3;
        for (int i = 0; i < refinementStationUnlocked.Length; i++)
        {
            refinementStationUnlocked[i] = true;
        }
    }
}

[Serializable]
public class SaveData
{
    public string profileName = PlayerProfile.profileName;
    public List<PotionData> acquiredPotion = PlayerProfile.acquiredPotion;
    public CustomerProfile[] customerProfile = PlayerProfile.customerProfile;
}
