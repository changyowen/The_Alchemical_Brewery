using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPotionHandler : MonoBehaviour
{
    public static ChosenPotionHandler Instance { get; private set; }

    public GameObject[] chosenPotion_obj;
    public Transform choosingPotionContainer_transform;

    public List<PotionData> chosenPotionList;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateChosenPotion()
    {
        for (int i = 0; i < chosenPotion_obj.Length; i++)
        {
            ChosenPotionInformation currentChosenPotionInfomation = chosenPotion_obj[i].GetComponent<ChosenPotionInformation>();
            if (i < chosenPotionList.Count)
            {
                currentChosenPotionInfomation.UpdatePanelInformation(chosenPotionList[i]);
            }
            else
            {
                currentChosenPotionInfomation.UpdateEmptyInformation();
            }
        }
    }

    public void RemoveChosenPotion(int index)
    {
        if(chosenPotionList.Count > index)
        {
            ///REMOVE POTION DATA
            chosenPotionList.RemoveAt(index);

            ///UPDATE CHOOSING POTION PANEL
            int childs = choosingPotionContainer_transform.childCount;
            for (int i = 0; i < childs; i++)
            {
                PotionInformationHandler potionInformationHandler = choosingPotionContainer_transform.GetChild(i).GetComponent<PotionInformationHandler>();
                potionInformationHandler.UpdateAlreadyChosen();
            }

            ///UPDATE CHOSEN POTION PANEL
            UpdateChosenPotion();
        }
        
    }
}
