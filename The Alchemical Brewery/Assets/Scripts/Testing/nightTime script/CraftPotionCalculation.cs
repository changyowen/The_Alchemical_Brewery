using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotionCalculation : MonoBehaviour
{
    public static CraftPotionCalculation Instance { get; private set; }

    public int PotionCalculate(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);

        //typeVariabe calculation
        int totalScore = 0;
        for (int i = 0; i < ingredientDataList.Count - 1; i++)
        {
            for (int j = i + 1; j < ingredientDataList.Count; j++)
            {
                totalScore += ingredientDataList[i].typeVariable * ingredientDataList[j].typeVariable;
            }
        }

        return totalScore;
    }

    List<IngredientData> GetIngredientData(List<int> potIngredient)
    {
        //assignn ingedient data
        List<IngredientData> ingredientDataList = new List<IngredientData>();
        for (int i = 0; i < potIngredient.Count; i++)
        {
            ingredientDataList.Add(SO_Holder.Instance.ingredientSO[potIngredient[i]]);
        }

        //return ingredient data list
        return ingredientDataList;
    }
}
