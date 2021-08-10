using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public static class PlayerProfile 
{
    public static string profileName = "NewPlayer";
    public static List<PotionData> acquiredPotion = new List<PotionData>();
    public static CustomerProfile[] customerProfile = new CustomerProfile[10];
    public static ShopProfile shopProfile = new ShopProfile();

    public static void LoadData(SaveData saveData)
    {
        profileName = saveData.profileName;
        acquiredPotion = saveData.acquiredPotion;
        customerProfile = saveData.customerProfile;
    }

    public static void NewGameData(string _profileName)
    {
        profileName = _profileName;

        for (int i = 0; i < customerProfile.Length; i++)
        {
            CustomerProfile newCustomerProfile = new CustomerProfile();
            customerProfile[i] = newCustomerProfile;
        }
        UnlockNewCustomer(0);
    }

    public static void UnlockNewCustomer(int customerIndex)
    {
        customerProfile[customerIndex].unlocked = true;
    }

    public static void UnlockNewIngredient(int ingredientIndex)
    {

    }

    public static void UnlockNewPot()
    {
        shopProfile.potUnlocked++;
    }

    public static void UnlockNewCounter()
    {
        shopProfile.counterUnlocked++;
    }

    public static void UnlockRefinementStation()
    {
        shopProfile.refinementStationUnlocked++;
    }

    public static void GM_TestingUse()
    {
        profileName = "SuperLegzai";

        for (int i = 0; i < customerProfile.Length; i++)
        {
            CustomerProfile newCustomerProfile = new CustomerProfile();
            customerProfile[i] = newCustomerProfile;
        }
        UnlockNewCustomer(0);
    }
}

[Serializable]
public class ShopProfile
{
    [Range(1, 3)] public int counterUnlocked = 1;
    [Range(1, 10)] public int herbSlotUnlocked = 1;
    [Range(1, 3)] public int potUnlocked = 1;
    [Range(1, 2)] public int refinementStationUnlocked = 1;

    public bool decreaseCustomerAppearRate = false;
}

[Serializable]
public class SaveData
{
    public string profileName = PlayerProfile.profileName;
    public List<PotionData> acquiredPotion = PlayerProfile.acquiredPotion;
    public CustomerProfile[] customerProfile = PlayerProfile.customerProfile;
}
