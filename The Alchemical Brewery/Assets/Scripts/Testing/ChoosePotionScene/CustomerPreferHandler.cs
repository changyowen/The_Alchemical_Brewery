using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerPreferHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;
    public CustomerInformationHandler_ChoosePotionScene currentCustomerInformation;

    public Image canvasSprite;

    List<PotionData> chosenPotionList;
    float globalFavorPoint = 0;

    private void Update()
    {
        if(ChosenPotionHandler.Instance != null)
        {
            chosenPotionList = ChosenPotionHandler.Instance.chosenPotionList;
        }

        //update favor point
        if(chosenPotionList != null)
        {
            globalFavorPoint = 0;
            UpdateFavorPoint();
            UpdateCustomerCanvasSprite();
        }
        
    }

    void UpdateFavorPoint()
    {
        //check every chosen potion
        for (int i = 0; i < chosenPotionList.Count; i++)
        {
            //reset favorPoint
            int favorPoint = 0;

            //check prefer usage
            favorPoint += CheckPreferUsage(i);
            //check prefer element
            favorPoint += CheckPreferElement(i);

            if (globalFavorPoint < favorPoint)
            {
                globalFavorPoint = favorPoint;
            }
        }
    }

    int CheckPreferUsage(int _chosenPotionIndex)
    {
        //check every customer prefer usage
        for (int a = 0; a < currentCustomerInformation.customerDataSO.preferPotionUsage.Count; a++)
        {
            //get customer prefer usage
            PotionUsage _preferUsage = currentCustomerInformation.customerDataSO.preferPotionUsage[a];
            //check if chosen potion is customer preffered
            if (chosenPotionList[_chosenPotionIndex].potionUsage == _preferUsage)
            {
                return 1;
            }
        }
        return 0;
    }

    int CheckPreferElement(int _chosenPotionIndex)
    {
        int favorPoint = 0;
        //check every customer prefer element
        for (int a = 0; a < currentCustomerInformation.customerDataSO.preferElement.Count; a++)
        {
            //get customer prefer element
            Element _preferElement = currentCustomerInformation.customerDataSO.preferElement[a];
            //check if chosen potion contain this element or not
            if (chosenPotionList[_chosenPotionIndex].potionElement.Contains(_preferElement))
            {
                favorPoint++;
            }
        }
        return favorPoint;
    }

    void UpdateCustomerCanvasSprite()
    {
        if(chosenPotionList.Count == 0)
        {
            canvasSprite.sprite = SO_holder.questionMarkLogoSprite;
        }
        else
        {
            //total point = (total prefer element) + (1 prefer usage)
            float totalPoint = (float)currentCustomerInformation.customerDataSO.preferElement.Count + 1;
            float newFavorPoint = globalFavorPoint / totalPoint;

            if(newFavorPoint >= 1)
            {
                canvasSprite.sprite = SO_holder.loveLogoSprite;
            }
            else if(newFavorPoint < 1f && newFavorPoint >= .5f)
            {
                canvasSprite.sprite = SO_holder.okayLogoSprite;
            }
            else if(newFavorPoint < .5f)
            {
                canvasSprite.sprite = SO_holder.boredLogoSprite;
            }
        }
    }
}
