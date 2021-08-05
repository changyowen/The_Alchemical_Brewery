using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMono : MonoBehaviour
{
    int a = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            PotionData newPotionData = new PotionData();
            newPotionData.potionName = "potion" + a;
            newPotionData.potionFormular = new int[] { a, a, a, a };
            PlayerProfile.acquiredPotion.Add(newPotionData);
            Debug.Log(PlayerProfile.acquiredPotion.Count);
            a++;
            SaveManager.Save();
            Debug.Log("Save");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveManager.Load();
            Debug.Log("Load");
            Debug.Log(PlayerProfile.acquiredPotion.Count);
            Debug.Log(PlayerProfile.acquiredPotion[PlayerProfile.acquiredPotion.Count - 1].potionFormular[3]);
        }
    }
}
