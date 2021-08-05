using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotIngredientHolderHandler : MonoBehaviour
{
    public Image[] potIngredientHolder;

    // Update is called once per frame
    void Update()
    {
        //get pot ingredient list
        List<int> potIngredientList = CraftPotionManager.Instance.potIngredientList;

        for (int i = 0; i < 4; i++)
        {
            if (i < potIngredientList.Count)
            {
                IngredientData ingData = SO_Holder.Instance.ingredientSO[potIngredientList[i]];
                potIngredientHolder[i].sprite = ingData.ingredientSprite;
            }
            else
            {
                potIngredientHolder[i].sprite = SO_Holder.Instance.transparentSprite;
            }
        }
    }
}
