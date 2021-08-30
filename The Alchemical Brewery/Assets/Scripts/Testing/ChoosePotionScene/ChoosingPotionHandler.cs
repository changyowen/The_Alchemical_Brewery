using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingPotionHandler : MonoBehaviour
{
    public static ChoosingPotionHandler Instance { get; private set; }

    public GameObject potionInformation_obj;
    public Transform potionInformationContainer;

    List<PotionData> availablePotionList = new List<PotionData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AssignAllAvailablePotion()
    {
        for (int i = 0; i < PlayerProfile.acquiredPotion.Count; i++)
        {
            availablePotionList.Add(PlayerProfile.acquiredPotion[i]);
        }
    }

    public void InstantiateAvailablePotion()
    {
        for (int i = 0; i < availablePotionList.Count; i++)
        {
            //instantiate potion information
            GameObject newPotionInformation = Instantiate(potionInformation_obj, Vector3.zero, Quaternion.identity) as GameObject;
            newPotionInformation.transform.SetParent(potionInformationContainer, false);

            //update potion information handler
            PotionInformationHandler potionInformationHandler = newPotionInformation.GetComponent<PotionInformationHandler>();
            potionInformationHandler.UpdateDataInformation(availablePotionList[i]);
        }
    }
}
