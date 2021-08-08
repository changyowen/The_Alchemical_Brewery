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
public class SaveData
{
    public string profileName = PlayerProfile.profileName;
    public List<PotionData> acquiredPotion = PlayerProfile.acquiredPotion;
    public CustomerProfile[] customerProfile = PlayerProfile.customerProfile;
}
