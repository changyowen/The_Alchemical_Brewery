using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public static class PlayerProfile 
{
    public static string profileName = "NewPlayer";
    public static string lastSave = "12/12/1999";
    public static int dayCount = 1;
    public static int dayResetTravel = 0;
    public static int stageChosen = 1;
    public static int cashTotal = 10000;
    public static List<PotionData> acquiredPotion = new List<PotionData>();
    public static IngredientProfile[] ingredientProfile = new IngredientProfile[60];
    public static CustomerProfile[] customerProfile = new CustomerProfile[8];
    public static FairyShopProfile fairyShopProfile = new FairyShopProfile();
    public static ShopProfile shopProfile = new ShopProfile();

    public static void LoadData(SaveData saveData)
    {
        profileName = saveData.profileName;
        lastSave = saveData.lastSave;
        dayCount = saveData.dayCount;
        dayResetTravel = saveData.dayResetTravel;
        cashTotal = saveData.cashTotal;
        stageChosen = saveData.stageChosen;
        acquiredPotion = saveData.acquiredPotion;
        ingredientProfile = saveData.ingredientProfile;
        customerProfile = saveData.customerProfile;
        fairyShopProfile = saveData.fairyShopProfile;
        shopProfile = saveData.shopProfile;
    }

    public static void NewGameData(string _profileName)
    {
        ///ASSIGN PROFILE NAME
        profileName = _profileName;
        ///ASSIGN DAY COUNT, DAY RESET TRAVEL, STAGE CHOSEN
        dayCount = 1;
        dayResetTravel = 0;
        stageChosen = 1;
        ///ASSIGN CASH
        cashTotal = 10000;

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

        ///ASSIGN CASH
        cashTotal = 1000000;

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
        for (int i = 0; i < 8; i++)
        {
            customerProfile[i].UnlockThisCustomer();
        }
        //unlock first 4 ingredients
        for (int i = 0; i < 60; i++)
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
    public int[] ingredientPurchased = new int[60];

    [Range(1, 3)] public int counterPurchased = 1;
    [Range(1, 10)] public int magicChestPurchased = 4;
    [Range(1, 3)] public int potPurchased = 1;
    public bool[] refinementStationPurchased = new bool[2] { false, false };

    public bool increaseCustomerAppearRate = false;

    public void PurchaseAll()
    {
        for (int i = 0; i < ingredientPurchased.Length; i++)
        {
            ingredientPurchased[i] = 75;
        }

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
    public string profileName;
    public string lastSave;
    public int dayCount;
    public int dayResetTravel;
    public int stageChosen;
    public int cashTotal;
    public List<PotionData> acquiredPotion;
    public IngredientProfile[] ingredientProfile;
    public CustomerProfile[] customerProfile;
    public FairyShopProfile fairyShopProfile;
    public ShopProfile shopProfile;

    public void DirectSave()
    {
        profileName = PlayerProfile.profileName;
        lastSave = PlayerProfile.lastSave;
        dayCount = PlayerProfile.dayCount;
        dayResetTravel = PlayerProfile.dayResetTravel;
        stageChosen = PlayerProfile.stageChosen;
        cashTotal = PlayerProfile.cashTotal;
        acquiredPotion = PlayerProfile.acquiredPotion;
        ingredientProfile = PlayerProfile.ingredientProfile;
        customerProfile = PlayerProfile.customerProfile;
        fairyShopProfile = PlayerProfile.fairyShopProfile;
        shopProfile = PlayerProfile.shopProfile;
    }
}
