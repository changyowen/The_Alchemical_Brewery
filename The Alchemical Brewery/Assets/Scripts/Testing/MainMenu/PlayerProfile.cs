using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public static class PlayerProfile 
{
    public static string profileName = "NewPlayer";
    public static List<PotionData> acquiredPotion = new List<PotionData>();

    public static void LoadData(SaveData saveData)
    {
        profileName = saveData.profileName;
        acquiredPotion = saveData.acquiredPotion;
    }

    public static void GM_TestingUse()
    {
        profileName = "SuperLegzai";

    }
}

[Serializable]
public class SaveData
{
    public string profileName = PlayerProfile.profileName;
    public List<PotionData> acquiredPotion = PlayerProfile.acquiredPotion;
}
