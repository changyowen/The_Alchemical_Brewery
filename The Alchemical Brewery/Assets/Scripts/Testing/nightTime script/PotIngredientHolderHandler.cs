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
                //get so_Holder
                ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
                //get ingredient data
                IngredientData ingData = so_Holder.ingredientSO[potIngredientList[i]];
                potIngredientHolder[i].sprite = ingData.ingredientSprite;
            }
            else
            {
                //get so_Holder
                ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
                //get trasparent sprite
                potIngredientHolder[i].sprite = so_Holder.transparentSprite;
            }
        }
    }
}
