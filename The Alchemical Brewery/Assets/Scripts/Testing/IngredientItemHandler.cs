using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientItemHandler : MonoBehaviour
{
    public int shelfIndex = 0;
    public int ingredientIndex = 0;

    public void ReceiveIngredient()
    {
        //receive ingredient
        PlayerInfoHandler.Instance.playerIngredientHolder.Add(ingredientIndex);
    }
}
